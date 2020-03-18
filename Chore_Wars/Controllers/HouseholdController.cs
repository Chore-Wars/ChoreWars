using System;
using System.Collections.Generic;
using System.Linq;
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


        public IActionResult RegisterHouseHold(int id)
        {
            return View();
        }

        //public IActionResult ViewHouseHoldMembers(String Member)
        //{

        //    return View();
        //}

        public IActionResult ViewPlayers()
        {
            //  var players = _context.Player.Where(x => x.HouseholdId == null);
            var players = _context.Player.Where(x => x.UserId != null).ToList();
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
            _context.Player.Add(newPlayer);
            _context.SaveChanges();
            return RedirectToAction();
        }

        //public IActionResult ViewHouseHoldChores(string Chores)
        //{
        //    return View();
        //}

    }
}