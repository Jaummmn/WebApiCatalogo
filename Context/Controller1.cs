using Microsoft.AspNetCore.Mvc;

namespace WebApiCurso.Context;

public class Controller1 : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}