using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chore_Wars.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chore_Wars.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ChoreWarsDbContext _context;

        public PlayerController(ChoreWarsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddPlayer()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddPlayer(Player newPlayer)
        {
            if (ModelState.IsValid)
            {
                //check which IDENTITY user is logged in.
                //do... newPlayer.HouseholdId = (identity stuff)
                _context.Player.Add(newPlayer);
                _context.SaveChanges();
            }
            return View();
        }

        //public IActionResult DeletePlayer(int id)
        //{
        //    Player found = _context.Player.Find(id);
        //    if (found != null)
        //    {
        //        List<Player> thisPlayer = _context.Player.Remove()
        //        //List<Player> thisPlayer = _context.Player.Where(x => x.UserId == found.UserId).ToList();
        //        if (thisPlayer != null)
        //        {
        //            foreach (Player player in thisPlayer)
        //            {
        //                player.UserId = null;
        //            }
        //        }

        //        _context.Player.Remove(found);
        //        _context.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}