using System.Collections.Generic;
using System.Linq;
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
                return RedirectToAction("ViewChores");
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
        public IActionResult EditChore(int id)
        {
            Chore found = _context.Chore.Find(id);
            if (found != null)
            {
                return View(found);
            }
            return RedirectToAction("ViewChores");
        }

        [HttpPost]
        public IActionResult EditChore(Chore editedChore)
        {
            Chore dbChore = _context.Chore.Find(editedChore.ChoreId);
            if (ModelState.IsValid)
            {
                dbChore.PointValue = editedChore.PointValue;
                dbChore.ChoreName = editedChore.ChoreName;
                dbChore.ChoreDescription = editedChore.ChoreDescription;

                _context.Entry(dbChore).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(dbChore);
                _context.SaveChanges();
            }
            return RedirectToAction("ViewChores");
        }





        //assign chores to player. subtract points

        [HttpGet]
        public IActionResult AssignChore(int id)
        {
            Chore found = _context.Chore.Find(id);

           
                return View(found);
            

        }





    }
}