using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Washyn.SbTheme.Bundling;

public class BasicThemeGlobalScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/js/scripts.js");
    }
}
