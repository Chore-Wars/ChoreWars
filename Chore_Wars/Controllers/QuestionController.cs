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
        //    if (playerListJson != null)
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

        private List<Player> allPlayers = new List<Player>();

        public IActionResult TestIndex()
        {
            PopulateFromSession();
            return View(allPlayers);
        }

        public IActionResult SavePlayer(Player newPlayer)
        {
            PopulateFromSession();
            allPlayers.Add(newPlayer);

            HttpContext.Session.SetString("AllPlayerSession", JsonConvert.SerializeObject(allPlayers));
            //HttpContext.Session.SetString("User", userName);
            return RedirectToAction("TestIndex");
        }

        public IActionResult ClearPlayers()
        {
            HttpContext.Session.Remove("AllPlayerSession");
            return RedirectToAction("TestIndex");
        }

        public void PopulateFromSession()
        {
            string playerListJson = HttpContext.Session.GetString("AllPlayerSession");
            if(playerListJson != null)
            {
                allPlayers = JsonConvert.DeserializeObject<List<Player>>(playerListJson);
            }
        }
        //^TESTING^

        

        [HttpGet]
        public IActionResult SelectQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SelectQuestion(string difficulty)
        {
            TempData["difficulty"] = difficulty;
            return RedirectToAction("GetQuestion", "Question");
        }



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
            Player foundPlayer = _context.Player.Find();

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