using Localization.Resources.AbpUi;
using Volo.Abp.UI.Navigation;

namespace Washyn.SbTheme.Menus;

public class PublicMenuContributor : IMenuContributor
{
    public const string PublicMenu = "Public";
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == PublicMenu)
        {
            await ConfigureMainMenuAsync(context);
        }
    }
    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<AbpUiResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "BookstoreMenus.Home",
                l["Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "BookstoreMenus.Test",
                l["Test"],
                "#",
                icon: "fa fa-home",
                order: 2
            )
        );
        
        return Task.CompletedTask;
    }
}