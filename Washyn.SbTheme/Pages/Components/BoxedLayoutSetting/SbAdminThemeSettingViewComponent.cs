using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;
using Volo.Abp.Settings;

namespace Washyn.SbTheme.Pages.Components.BoxedLayoutSetting;

public class SbAdminThemeSettingViewComponent : AbpViewComponent
{
    private readonly ISettingManager _settingManager;

    public SbAdminThemeSettingViewComponent(ISettingManager settingManager)
    {
        _settingManager = settingManager;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new UpdateSbThemeSettingViewModel()
        {
            BoxedLayout = Convert.ToBoolean(await _settingManager.GetOrNullForCurrentUserAsync(SbAdminThemeSettingNames.Layout.Boxed)),
            DarkNavigationBar = Convert.ToBoolean(await _settingManager.GetOrNullForCurrentUserAsync(SbAdminThemeSettingNames.Layout.DarkNavigationBar)),
        };
        return View("~/Pages/Components/BoxedLayoutSetting/Default.cshtml", model);
    }
}

[Route("api/setting/sb-theme")]
public class SbThemeController : AbpControllerBase
{
    private readonly ISettingManager _settingManager;

    public SbThemeController(ISettingManager settingManager)
    {
        _settingManager = settingManager;
    }
    
    [HttpGet]
    public async Task<UpdateSbThemeSettingViewModel> GetAsync()
    {
        var res = new UpdateSbThemeSettingViewModel();
        res.BoxedLayout = Convert.ToBoolean(await _settingManager.GetOrNullForCurrentUserAsync(SbAdminThemeSettingNames.Layout.Boxed));
        res.DarkNavigationBar = Convert.ToBoolean(await _settingManager.GetOrNullForCurrentUserAsync(SbAdminThemeSettingNames.Layout.DarkNavigationBar));
        return res;
    }
    
    [HttpPost]
    public async Task UpdateAsync(UpdateSbThemeSettingViewModel model)
    {
        await _settingManager.SetForCurrentTenantAsync(SbAdminThemeSettingNames.Layout.Boxed, model.BoxedLayout.ToString());
        await _settingManager.SetForCurrentTenantAsync(SbAdminThemeSettingNames.Layout.DarkNavigationBar, model.DarkNavigationBar.ToString());
    }
}

public class UpdateSbThemeSettingViewModel
{
    public bool BoxedLayout { get; set; }
    public bool DarkNavigationBar { get; set; }
}

public static class SbAdminThemeSettingNames
{
    private const string DefaultPrefix = "Washyn.SbAdmin";
    // public const string Style = DefaultPrefix + ".Style";
    // public const string PublicLayoutStyle = DefaultPrefix + ".Style.PublicLayout";
    public static class Layout
    {
        private const string LayoutPrefix = DefaultPrefix + ".Layout";
        public const string Boxed = LayoutPrefix + ".Boxed";
        public const string DarkNavigationBar = LayoutPrefix + ".DarkNavigationBar";
    }
}

public class SbThemeSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(SbAdminThemeSettingNames.Layout.Boxed,
            true.ToString(),
            L("Boxed layout"))
        );
        
        context.Add(new SettingDefinition(SbAdminThemeSettingNames.Layout.DarkNavigationBar,
            true.ToString(),
            L("Dark navigation bar"))
        );
    }
    
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SbThemeResource>(name);
    }
}


[LocalizationResourceName("SbTheme")]
public class SbThemeResource
{

}

public class SbThemeSettingPageContributor : ISettingPageContributor
{
    public async Task ConfigureAsync(SettingPageCreationContext context)
    {
        if (await CheckPermissionsAsync(context))
        {
            context.Groups.Add(
                new SettingPageGroup(
                    "Washyn.SbAdmin.ThemeSetting",
                    "Theme config",
                    typeof(SbAdminThemeSettingViewComponent)
                )
            );
        }
    }

    public async Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
    {
        // var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
        // return await authorizationService.IsGrantedAsync(CompanyPermissions.Company.Default);
        return true;
    }
}

[DependsOn(typeof(Volo.Abp.SettingManagement.Web.AbpSettingManagementWebModule))]
public class SbThemeSettingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SettingManagementPageOptions>(options =>
        {
            options.Contributors.AddFirst(new SbThemeSettingPageContributor());
        });
    }
}