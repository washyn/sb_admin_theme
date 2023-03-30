using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Washyn.SbTheme.Bundling;
using Washyn.SbTheme.Menus;
using Washyn.SbTheme.Pages.Components.BoxedLayoutSetting;
using Washyn.SbTheme.Toolbars;

namespace Washyn.SbTheme;

[DependsOn(typeof(SbThemeSettingModule))]
[DependsOn(typeof(AbpAspNetCoreMvcUiThemeSharedModule))]
[DependsOn(typeof(AbpAspNetCoreMvcUiMultiTenancyModule))]
public class WashynSbThemeModule : AbpModule
{
    
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WashynSbThemeModule).Assembly);
        });
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new PublicMenuContributor());
        });
        
        Configure<AbpThemingOptions>(options =>
        {
            options.Themes.Add<SbTheme>();

            if (options.DefaultThemeName == null)
            {
                options.DefaultThemeName = SbTheme.Name;
            }
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WashynSbThemeModule>();
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new BasicThemeMainTopToolbarContributor());
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(SbThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(StandardBundles.Styles.Global)
                        .AddContributors(typeof(BasicThemeGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(SbThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(StandardBundles.Scripts.Global)
                        .AddContributors(typeof(BasicThemeGlobalScriptContributor));
                });
        });
    }
}