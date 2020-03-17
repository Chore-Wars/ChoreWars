using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chore_Wars.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chore_Wars.Controllers
{
    public class ChoreController : Controller
    {

        private readonly ChoreWarsDbContext _context;
        public ChoreController(ChoreWarsDbContext context)
        {
            _context = context;
        }
        //add chores to database
        [HttpGet]
        public IActionResult AddChore()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddChore(Chore newChore)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(newChore);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult BuyChoresFor(int userid)
        {
            return View();
        }


        public IActionResult ViewAssignedChores(int userid)
        {
            return View();
        }


        public IActionResult EditChore()
        {
            return View();
        }

        public IActionResult DeleteChore()
        {
            return View();
        }
    }
}