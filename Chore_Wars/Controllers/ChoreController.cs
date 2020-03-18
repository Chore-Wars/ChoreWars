using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chore_Wars.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chore_Wars.Controllers
{
    public class ChoreController : Controller
    {
        List<Chore> thisHousholdsChores = new List<Chore> { };

        private readonly ChoreWarsDbContext _context;
        public ChoreController(ChoreWarsDbContext context)
        {
            _context = context;
        }

        //display chores in table
        public IActionResult ViewChores()
        {
            return View(_context.Chore.ToList());
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
            }
                return RedirectToAction("ViewChores", newChore);
        }


        //Delete Chore Method
        public IActionResult DeleteChore(int id)
        {
            Chore Found = _context.Chore.Find(id);
            if (Found != null)
            {
                _context.Chore.Remove(Found);
                _context.SaveChanges();
            }
            return RedirectToAction("ViewChores");
        }

        //Edit chore method
        //public IActionResult EditChore(int id)
        //{
        //    Chore found = _context.Chore.Find(id);
        //    if (found != null)
        //    {

        //        //modify the state of this entry in the database
        //        _context.Entry(found).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        _context.Update(found);
        //        _context.SaveChanges();
        //    }
        //    //return RedirectToAction("ViewChores");
        //    return View();
        //}





        //assign chores to player. subtract points

        //public IActionResult BuyChoresFor(int userid)
        //{
        //    return View();
        //}





    }
}