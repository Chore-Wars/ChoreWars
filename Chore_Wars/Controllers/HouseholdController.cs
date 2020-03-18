using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chore_Wars.Controllers
{
    public class HouseHoldController : Controller
    {
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
            return RedirectToAction("SelectQuestion", "Question");
        }

        public IActionResult ViewHouseHoldChores(string Chores)
        {
            return View();
        }
    }
}
//LoginHousehold() <- Identity(mostly)
//RegisterHouseHold() <- Enter household name(‘The Cooper Family’)
//ViewHouseHoldMembers()
//ViewHouseHoldChores()
