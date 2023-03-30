using Microsoft.AspNetCore.Mvc;

namespace Washyn.SbTheme.Themes.Sb.Components.AccountLanguaje;

public class AccountLanguajeViewComponent : ViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Themes/Sb/Components/AccountLanguaje/Default.cshtml");
    }
}