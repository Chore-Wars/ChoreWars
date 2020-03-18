using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Chore_Wars.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chore_Wars.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ChoreWarsDbContext _context;

        public QuestionController(ChoreWarsDbContext context)
        {
            _context = context;
        }

        //TESTING

        //public IActionResult TestIndex()
        //{
        //    PopulateFromSession();
        //    return View(houseHoldPlayers);
        //}

        //public IActionResult SessionTest(Player newPlayer)
        //{
        //    PopulateFromSession();
        //    houseHoldPlayers.Add(newPlayer);

        //    HttpContext.Session.SetString("Player", JsonConvert.SerializeObject(houseHoldPlayers));
        //    //var playerList = _context.Player.ToList();
        //    return RedirectToAction("TestIndex", houseHoldPlayers);
        //}
        //public void PopulateFromSession()
        //{
        //    string playerListJson = HttpContext.Session.GetString("Players");
        //    {
        //        houseHoldPlayers = JsonConvert.DeserializeObject<List<Player>>(playerListJson);
        //    }
        //}

        public void AddDummyPlayer()
        {
            Player dummyPlayer = new Player();
            dummyPlayer.FirstName = "Jeff";
            dummyPlayer.LastName = "Jefferson";
            dummyPlayer.Age = 31;
            _context.Player.Add(dummyPlayer);
            _context.SaveChanges();
        }

        //need to set up a 'dummy' list or object in order to contain the data we'll store 
        //in our case, we probably only need to store a single Player at a time
            //to represent the 'logged in' Player

        //Test action/view to contain
        //public IActionResult TestIndex()
        //{
        //    PopulateFromSession();
        //    return View(allPlayers);
        //}

        ////Test action to save session data
        ////in this case: 
        //private List<Player> allPlayers = new List<Player>();
        ////1) Creating a new Player (from information provided by the view)
        ////2) Adding that Player to our 'dummy' List
        ////3) Setting the new Session string equal to the new List (SetString), which just received a new Player
        ////4) Sending back to TestIndex view, where list is displayed
        //public IActionResult SavePlayer(Player newPlayer)
        //{
        //    //calling PopulateFromSession here in order to store multiple items at once.
        //    //likely won't have to do that in a 'live' environment - only one Player will ever be 'logged in' at once     
        //    PopulateFromSession();
        //    allPlayers.Add(newPlayer);

        //    //So... here, we're... Basically sessions have 2 'modes' - .SetString, and .SetInt32
        //    //JsonConvert.SerializeObject converts the object (allPlayers) into JSON(string) format
        //     //so... take this session called "AllPlayerSession", and set it to the new JSON-ified allPlayers list object
        //    HttpContext.Session.SetString("AllPlayerSession", JsonConvert.SerializeObject(allPlayers));
        //    return RedirectToAction("TestIndex");
        //}

        public IActionResult ClearPlayers()
        {
            //clears the current session named "AllPlayerSession"
            HttpContext.Session.Remove("AllPlayerSession");
            return RedirectToAction("TestIndex");
        }



        //public void PopulateFromSession()
        //{
        //    //tries to get the "AllPlayerSession" as a string. If it exists, de JSON-ify that object
        //    //and re-instantiate(?) it as an object of type List<Player>
        //    //if the "AllPlayerSession" JSON-ified situation is blank (null), do nothing.
        //    string playerListJson = HttpContext.Session.GetString("AllPlayerSession");
        //    if(playerListJson != null)
        //    {
        //        allPlayers = JsonConvert.DeserializeObject<List<Player>>(playerListJson);
        //    }
        //}
        ////^TESTING^

      

        [HttpGet]
        public IActionResult SelectQuestion()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult SelectQuestion(string difficulty)
        //{
        //    PopulateFromSession();
        //    TempData["difficulty"] = difficulty;
        //    return RedirectToAction("GetQuestion", "Question");
        //}



        public async Task<IActionResult> GetQuestion(string difficulty)
        {
            //api call. Additional logic for question filtering would go below
            //this could get gross. probably build a helper method if we have a lot of filtering options
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://opentdb.com/api.php");

            //loads tempdata from SelectQuestion action, to retrieve correct question type.
            var loadDifficulty = TempData["difficulty"];
            var response = await client.GetAsync($"?amount=1&difficulty={loadDifficulty}&type=multiple");
            var question = await response.Content.ReadAsAsync<ApiQuestion>();
            
            //mixes up answers and assigns them to the all_answers property (see ApiQuestion class)
            question.results[0].ScrambleAnswers(question.results[0].correct_answer, question.results[0].incorrect_answers);

            //determine point value based on question difficulty
            if (question.results[0].difficulty == "easy")
            {
                question.results[0].point_value = 3;
            }
            else if (question.results[0].difficulty == "medium")
            {
                question.results[0].point_value = 5;
            }
            else
            {
                question.results[0].point_value = 8;
            }

            return View(question);
        }

        public IActionResult Result(string selection, string answer, int points)
        {
            string outcome;

         //   Player foundPlayer = _context.Player.Find();

            //evaluates Player selection. Awards points if correct, and increments WrongAnswer if incorrect
            if (selection == answer)
            {
                //add logic to add points to DB, based on current Player
                //foundPlayer.CurrentPoints += points;
                //foundPlayer.TotalPoints += points;
                
                outcome = ($"Correct! you earned {points} points!");
                return View("Result", outcome);
            }
            else
            {
                //add logic to increment 'WrongAnswer' in Player db table, based on current Player
                //foundPlayer.IncorrectAnswers += 1;
                
                outcome = "Incorrect :(";
                return View("Result", outcome);
            }
            
        }
    }
}