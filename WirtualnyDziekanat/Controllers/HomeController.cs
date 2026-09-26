using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VirtualDeansOffice.Models;
using VirtualDeansOffice.Queries;

namespace VirtualDeansOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly StatisticsHandler _statistics;

        public HomeController(StatisticsHandler statistics)
        {
            _statistics = statistics;
        }

        public IActionResult Index()
        {
            var statistics = _statistics.Handle(new StatisticsQuery());
            return View(statistics);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
