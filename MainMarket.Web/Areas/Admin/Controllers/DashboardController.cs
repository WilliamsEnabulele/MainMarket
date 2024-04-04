using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Web.Areas.Admin.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}