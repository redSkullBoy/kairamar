using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TgBot.Controllers;
public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
