using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chore_Wars.Controllers
{
    public class HouseHoldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginHouseHold()
        {
            return View();
        }
        public IActionResult RegisterHouseHold()
        {
            return View();
        }
        public IActionResult ViewHouseHoldMembers()
        {
            return View();
        }

        public IActionResult ViewHouseHoldChores()
        {
            return View();
        }  
    }
}
//LoginHousehold() <- Identity(mostly)
//RegisterHouseHold() <- Enter household name(‘The Cooper Family’)
//ViewHouseHoldMembers()
//ViewHouseHoldChores()
