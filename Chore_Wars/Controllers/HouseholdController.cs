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
        public HouseHoldController(ChoreWarsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult RegisterHouseHold(int Id)
        {
            return View();
        }

        //present a list of members that exist inside the household
        //each household represented by the ASP Net user ID
        //Create a list "Players-where the ID" bundle list of users and display list of household players
        public IActionResult ViewHouseHoldMembers(String Member)
        {


            //view will have list of users
            //each user can have a button action to sign in with, each one a form with name/household/submit button goes to an action "Login HouseHoldMemmber"  
            //set up session, session value == to playerID
            //view all members, select with button, redirect to action, which difficulty lvl question, Session user gets points for correct answer or minues for incorrect.  
            //return redirect to action-
            return View();
        }
        public IActionResult LoginPlayer()
        {
            HttpContext.Session.SetString("PlayerSession", JsonConvert.SerializeObject(sessionPlayer));


            return RedirectToAction("SelectQuestion", "Question");
        }

        public IActionResult ViewHouseHoldChores(string Chores)
        {
            return View();
        }
        public IActionResult ViewPlayers()
        {
          //  var players = _context.Player.Where(x => x.HouseholdId == null);

            var players = _context.Player.Where(x => x.UserId != null).ToList();
            return View(players);
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
        
      //  public IActionResult StoreSessionPlayer 
        //dummy player for testing Sessions
      public Player sessionPlayer = new Player();
        public void PopulateFromSession()
        {
            //tries to get the "AllPlayerSession" as a string. If it exists, de JSON-ify that object
            //and re-instantiate(?) it as an object of type List<Player>
            //if the "AllPlayerSession" JSON-ified situation is blank (null), do nothing.
            string playerJson = HttpContext.Session.GetString("PlayerSession");
            if (playerJson != null)
            {
                sessionPlayer = JsonConvert.DeserializeObject<Player>(playerJson);
            }
        }

    }
}

//LoginHousehold() <- Identity(mostly)
//RegisterHouseHold() <- Enter household name(‘The Cooper Family’)
//ViewHouseHoldMembers()
//ViewHouseHoldChores()
