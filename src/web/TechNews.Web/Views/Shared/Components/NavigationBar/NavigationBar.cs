using Microsoft.AspNetCore.Mvc;

namespace TechNews.Web.Views.Components.NavigationBar;

public class NavigationBarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}