using Microsoft.AspNetCore.Mvc.Razor;

namespace Libertas.Source.Configurations;

public class RazorLocationExpander : IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        yield return "/Source/Views/Shared/{0}" + RazorViewEngine.ViewExtension;
        yield return "/Source/Views/Pages/{1}/{0}" + RazorViewEngine.ViewExtension;
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
}
