using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chore_Wars.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chore_Wars.Controllers
{
    public class HouseHoldController : Controller
    {
        private readonly ChoreWarsDbContext _context;
        private readonly Helper _helper;
        private readonly IHttpContextAccessor _contextAccessor;
        public HouseHoldController(ChoreWarsDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult RegisterHouseHold(string id)
        {
            Household newHouseHold = new Household();
            id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            newHouseHold.AspNetUsers = id;
            //so, we have the ASPNETUSER ID as a string. Honestly, let's just set a bonus column and pray

            return View();
        }

        //public IActionResult ViewHouseHoldMembers(String Member)
        //{

        //    return View();
        //}
        //public void RegisterHouseHold(string aspId)


        //so.... when we activate these almonds...we need to check:
        //Does this householdId already exist?
        //if not, create a new Household in our table (#1, 2, 3, 22, etc)


        public IActionResult ViewPlayers()
        {
            string aspId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var players = _context.Player.Where(x => x.PlayerStr1 == aspId).ToList();
            //var players = _context.Player.Where(x => x.UserId != null).ToList();
            return View(players);
        }

        public Player sessionPlayer = new Player();
        public IActionResult LoginPlayer(int id)
        {
            sessionPlayer = _context.Player.Find(id);
            HttpContext.Session.SetString("PlayerSession", JsonConvert.SerializeObject(sessionPlayer));
            return RedirectToAction("SelectQuestion", "Question");
        }

        [HttpGet]
        public IActionResult AddNewPlayer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewPlayer(Player newPlayer)
        {
            //newPlayer.HouseholdId = _context.Household.Find();

            newPlayer.PlayerStr1 = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _context.Player.Add(newPlayer);
            _context.SaveChanges();
            return RedirectToAction("ViewPlayers");
        }

        //public IActionResult ViewHouseHoldChores(string Chores)
        //{
        //    return View();
        //}

    }
}