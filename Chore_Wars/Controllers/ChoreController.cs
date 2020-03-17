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
        //public IActionResult AddChore(Chore newChore)
        //{
        //    newChore.ChoreId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    //newChore.ChoreId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    //newChore.ChoreId = 1;

        //    //if (ModelState.IsValid)
        //    //{
        //    _context.Chore.Add(newChore);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //    //}
        //    return RedirectToAction("ViewChores");
        //}


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
            return RedirectToAction("ViewChores");
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
            //return RedirectToAction("ViewChores");
            return View();
        }


        public IActionResult ViewChores()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Chore> thisHousholdsChores = _context.Chore.Where(x => x.ChoreId.ToString() == id).ToList();

            return View(thisHousholdsChores);
        }


        public IActionResult BuyChoresFor(int userid)
        {
            return View();
        }





    }
}