using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace Washyn.SbTheme;
[ThemeName(Name)]
public class SbTheme : ITheme, ITransientDependency
{
    public const string Name = "Sb";

    public virtual string GetLayout(string name, bool fallbackToDefault = true)
    {
        switch (name)
        {
            case StandardLayouts.Application:
                return "~/Themes/Sb/Layouts/Application.cshtml";
            case StandardLayouts.Account:
                return "~/Themes/Sb/Layouts/Account.cshtml";
            case StandardLayouts.Empty:
                return "~/Themes/Sb/Layouts/Empty.cshtml";
            case StandardLayouts.Public:
                return "~/Themes/Sb/Layouts/Public.cshtml";
            default:
                return fallbackToDefault ? "~/Themes/Sb/Layouts/Application.cshtml" : null;
        }
    }
}