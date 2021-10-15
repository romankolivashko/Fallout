using Microsoft.AspNetCore.Mvc;

namespace Fallout.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

  }
}