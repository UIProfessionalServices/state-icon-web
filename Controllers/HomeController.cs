using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Icon.Communication.Sid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using state_icon_web.Models;

namespace state_icon_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISidClient sidClient;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ISidClient sidClient)
        {
            _logger = logger;
            this.sidClient = sidClient;
            sidClient.JwtToken = "123";

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string ssn)
        {
            var viewModel = new Icon.Communication.Sid.FindSidViewModel
            {
                StateCode = "UT",
                Ssn = ssn,
                RequestingUserId = "1234",
                TerminalId = "1235"
            };

            var response = sidClient.FindSsnAsync(viewModel);

            ViewData["message"] = response.Result.ToJson();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
