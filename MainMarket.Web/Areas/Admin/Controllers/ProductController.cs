using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Web.Areas.Admin.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}