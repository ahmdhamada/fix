using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FixFactorSystem.Controllers
{
    public class AboutController1 : Controller
    {

        // GET: AboutController1


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contactus()
        {
            return View();
        }

    }
}
