using Microsoft.AspNetCore.Mvc;

namespace Washyn.SbTheme.Themes.Sb.Components.AccountBrand;

public class AccountBrandViewComponent : ViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Sb/Components/AccountBrand/Default.cshtml");
    }
}