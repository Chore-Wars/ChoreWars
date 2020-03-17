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
                _context.Chore.Add(newChore);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //Delete Chore Method
        public IActionResult DeleteChore(int id)
        {
            Chore Found = _context.Chore.Find(id);
            if (Found != null)
            {
                _context.Chore.Remove(Found);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        //Edit chore method


        public IActionResult EditChore(int id)
        {
            Chore found = _context.Chore.Find(id);
            if (found != null)
            {
                
                //modify the state of this entry in the database
                _context.Entry(found).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(found);
                _context.SaveChanges();
            }
            return View(found.ChoreId);
        }


        public IActionResult BuyChoresFor(int userid)
        {
            return View();
        }


        public IActionResult ViewAssignedChores(int userid)
        {
            return View();
        }



    }
}