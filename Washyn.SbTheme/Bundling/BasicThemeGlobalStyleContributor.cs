using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Washyn.SbTheme.Bundling;

public class BasicThemeGlobalStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/themes/sb/layout.css");
        context.Files.Add("/css/sb-admin-styles.css");
    }
}
