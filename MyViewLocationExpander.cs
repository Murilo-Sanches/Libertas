using Microsoft.AspNetCore.Mvc.Razor;

public class MyViewLocationExpander : IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        yield return "/CustomViewFolder/{1}/{0}.cshtml";
        yield return "/SharedFolder/{0}.cshtml";
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
}