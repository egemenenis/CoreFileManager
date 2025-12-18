using CoreFileManager.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoreFileManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDummyService _service;

        public HomeController(IDummyService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            Log.Information("Home/Index çaðrýldý");
            ViewBag.Info = _service.GetInfo();
            return View();
        }

        public IActionResult Error()
        {
            throw new Exception("Test exception");
        }
    }
}
