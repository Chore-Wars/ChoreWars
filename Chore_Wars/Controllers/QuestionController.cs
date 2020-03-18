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

        private readonly Helper _helper;
        private readonly IHttpContextAccessor _contextAccessor;
        public QuestionController(ChoreWarsDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }





        [HttpGet]
        public IActionResult SelectQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SelectQuestion(string difficulty)
        {
            //Helper helper = new Helper(_contextAccessor);
            //var player = helper.PopulateFromSession();
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
            Helper helper = new Helper(_contextAccessor);
            var player = helper.PopulateFromSession();
            var foundPlayer = _context.Player.Find(player.UserId);

            string outcome;

            //   Player foundPlayer = _context.Player.Find();

            //evaluates Player selection. Awards points if correct, and increments WrongAnswer if incorrect
            if (selection == answer)
            {
                if (ModelState.IsValid)
                {
                    if(foundPlayer.CurrentPoints == null) { foundPlayer.CurrentPoints = 0; };
                    foundPlayer.CurrentPoints = foundPlayer.CurrentPoints + points;
                    if(foundPlayer.TotalPoints == null) { foundPlayer.CurrentPoints = 0; };
                    foundPlayer.TotalPoints = foundPlayer.CurrentPoints + points;
                    if(foundPlayer.CorrectAnswers == null) { foundPlayer.CurrentPoints = 0; };
                    foundPlayer.CorrectAnswers += 1;

                    _context.Entry(foundPlayer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.Update(foundPlayer);
                    _context.SaveChanges();
                }
                outcome = ($"Correct! you earned {points} points!");
                return View("Result", outcome);
            }
            else
            {
                //add logic to increment 'WrongAnswer' in Player db table, based on current Player
                if (ModelState.IsValid)
                {
                    if(foundPlayer.IncorrectAnswers == null)
                    {
                        foundPlayer.IncorrectAnswers = 0;
                    }
                    foundPlayer.IncorrectAnswers = foundPlayer.IncorrectAnswers + 1;

                    _context.Entry(foundPlayer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.Update(foundPlayer);
                    _context.SaveChanges();
                }
                outcome = "Incorrect :(";
                return View("Result", outcome);
            }
        }
        public IActionResult TestView()
        {
            Helper helper = new Helper(_contextAccessor);
            var player = helper.PopulateFromSession();

            return View();
        }

    }
}