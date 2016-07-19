using Microsoft.AspNetCore.Mvc;

using AspNetCoreLogging.Models;
using AspNetCoreLogging.Filters;
using Newtonsoft.Json;

namespace AspNetCoreLogging.Controllers
{
    [ServiceFilter(typeof(PerformanceLogFilter))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            this.Request.HttpContext.Session.Set(
                "LoginEmployee",
                System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Employee() { FirstName = "Ryuichi", LastName = "Daigo" })));

            return View();
        }

        public IActionResult PerformanceProblem()
        {
            System.Threading.Thread.Sleep(5000);

            return View();
        }

        public IActionResult OccurException()
        {
            int.Parse("hello");

            return View();
        }

        public IActionResult Error(string id)
        {
            this.ViewBag.ErrorCode = id;
            return View();
        }
    }
}
