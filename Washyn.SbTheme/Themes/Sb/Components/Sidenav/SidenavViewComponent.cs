using Microsoft.AspNetCore.Mvc;

namespace Washyn.SbTheme.Themes.Sb.Components.Sidenav;

public class SidenavViewComponent: ViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Sb/Components/Sidenav/Default.cshtml");
    }
}