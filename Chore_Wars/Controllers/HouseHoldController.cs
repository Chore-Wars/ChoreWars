using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chore_Wars.Controllers
{
    public class HouseholdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}