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

        //READY FOR DEPECIATION - NOT BEING USED
        public IActionResult RegisterHouseHold(string id)
        {
            Household newHouseHold = new Household();
            id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            newHouseHold.AspNetUsers = id;
            return View();
        }

        //READY FOR DEPRECIATION
        public IActionResult ViewPlayers()
        {
            string aspId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var players = _context.Player.Where(x => x.PlayerStr1 == aspId).ToList();
            //var players = _context.Player.Where(x => x.UserId != null).ToList();
            return View(players);
        }

        //READY FOR DEPRECIATION
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
            //initialize all Player properties
            newPlayer.PlayerStr1 = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            newPlayer.CurrentPoints = 0;
            newPlayer.TotalPoints = 0;
            newPlayer.CorrectAnswers = 0;
            newPlayer.IncorrectAnswers = 0;
            newPlayer.ChoresComplete = 0;
            newPlayer.PlayerStr2 = "";
            newPlayer.PlayerInt1 = 0;
            newPlayer.PlayerInt2 = 0;
            
            _context.Player.Add(newPlayer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}