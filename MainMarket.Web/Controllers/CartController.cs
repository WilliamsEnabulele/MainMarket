using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Web.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
