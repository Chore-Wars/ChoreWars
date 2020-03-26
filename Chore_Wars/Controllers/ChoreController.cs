using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Chore_Wars.Models;
using Chore_Wars.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Chore_Wars.Controllers
{
    [Authorize]
    public class ChoreController : Controller
    {
        private readonly ChoreWarsDbContext _context;
        private readonly Helper _helper;
        private readonly IHttpContextAccessor _contextAccessor;

        public ChoreController(ChoreWarsDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        //display chores in table. view allows player to assign chores to others
        public IActionResult ViewChores()
        {
            string aspId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            ViewModelPlayerChore playerChore = new ViewModelPlayerChore();

            //gets list of chores in Household
            playerChore.Chores = (_context.Chore.Where(x => x.ChoreStr1 == aspId).ToList());

            //gets a list of players in the Household
            playerChore.Players = _context.Player.Where (x => x.PlayerStr1 == aspId).ToList();
            return View(playerChore);
        }

        //will become the new top-level link
        //this should not allow user to assign chores to other b/c it's a top-level view
        public IActionResult BuyChores()
        {
            Helper helper = new Helper(_contextAccessor);
            var player = helper.PopulateFromSession();
            var foundPlayer = _context.Player.Find(player.UserId);

            string aspId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ViewModelPlayerChore playerChore = new ViewModelPlayerChore();

            playerChore.LoggedInPlayer = foundPlayer;

            //gets list of chores in Household
            playerChore.Chores = (_context.Chore.Where(x => x.ChoreStr1 == aspId).ToList());

            //gets a list of players in the Household
            playerChore.Players = _context.Player.Where(x => x.PlayerStr1 == aspId).ToList();
            return View(playerChore);
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
            
            newChore.ChoreStr1 = User.FindFirst(ClaimTypes.NameIdentifier).Value;

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
            Chore found = _context.Chore.Find(id);
            if (found != null)
            {
                _context.Chore.Remove(found);
                _context.SaveChanges();
            }
            return RedirectToAction("ViewChores");
        }

        public IActionResult CompleteChore(int id)
        {
            Chore found = _context.Chore.Find(id);
            if (found != null)
            {
                _context.Chore.Remove(found);
                _context.SaveChanges();
            }
            return RedirectToAction("SelectQuestion", "Question");
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
                dbChore.DueDate = editedChore.DueDate;

                _context.Entry(dbChore).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(dbChore);
                _context.SaveChanges();
            }
            return RedirectToAction("ViewChores");
        }




        //assign chores to player. subtract points
        //take in choreId to assign it
        //take in userId to assign it to that person
        //take in the amount of points to be spent
        public IActionResult AssignChore(int choreId, int userId, int points)
        {

            Helper helper = new Helper(_contextAccessor);
            var player = helper.PopulateFromSession();
            var foundPlayer = _context.Player.Find(player.UserId);

            //subtract from players current points
            if (foundPlayer.CurrentPoints >= points)
            {
                foundPlayer.CurrentPoints = foundPlayer.CurrentPoints - points;
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
            //assign chore based on userId
            var assignedChore = _context.Chore.Find(choreId);
            var assignedPlayer = _context.Player.Find(userId);

            assignedChore.UserId = assignedPlayer.UserId;

            //update and save assigned chore with the player
            _context.Entry(assignedChore).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Update(assignedChore);

            //update and save database
            _context.Entry(foundPlayer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Update(foundPlayer);
            _context.SaveChanges();

            return RedirectToAction("BuyChores");
        }

        public IActionResult ErrorPage()
        {
            return View();
        }
    }

}