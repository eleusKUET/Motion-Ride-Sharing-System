using Microsoft.AspNetCore.Mvc;
using Motion.Models;
using System.Diagnostics;

namespace Motion.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}