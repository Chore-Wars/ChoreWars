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
            Helper helper = new Helper(_contextAccessor);
            var player = helper.PopulateFromSession();
            var foundPlayer = _context.Player.Find(player.UserId);
            return View(foundPlayer);
        }

        [HttpPost]
        public IActionResult SelectQuestion(string difficulty, string category)
        {
            //Helper helper = new Helper(_contextAccessor);
            //var player = helper.PopulateFromSession();
            TempData["difficulty"] = difficulty;
            TempData["category"] = category;
            return RedirectToAction("GetQuestion", "Question");
        }

        public async Task<IActionResult> GetQuestion()
        {
            var loadDifficulty = TempData["difficulty"];
            var loadCategory = TempData["category"];
            var question = await GetAPIQuestion(loadDifficulty, loadCategory);

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

        public async Task<ApiQuestion> GetAPIQuestion(Object tDifficulty, Object tCategory)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://opentdb.com/api.php");
            var response = await client.GetAsync($"?amount=1&difficulty={tDifficulty}&category={tCategory}&type=multiple");
            var question = await response.Content.ReadAsAsync<ApiQuestion>();

            return question;
        }

        public IActionResult Result(string selection, string answer, int points)
        {
            Helper helper = new Helper(_contextAccessor);
            var player = helper.PopulateFromSession();
            var foundPlayer = _context.Player.Find(player.UserId);

            string outcome;

            //evaluates Player selection. Awards points if correct, and increments WrongAnswer if incorrect
            if (selection == answer)
            {
                if (ModelState.IsValid)
                {
                    foundPlayer.CurrentPoints += points;
                    foundPlayer.TotalPoints += points;
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
                if (ModelState.IsValid)
                {
                    foundPlayer.IncorrectAnswers += 1;

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

        //build a method for customizing api calls
        //based on CATEGORY && DIFFICULTY

        /*
         * DEFAULT TO ALL CATEGORIES IF NONE SELECTED
         * 
         * EDUCATIONAL:
         * 10: Books - EDUCATIONAL
         * 13: Musicals/Theathre - EDUCATIONAL
         * 17: Science & Nature - EDUCATIONAL
         * 18: Science: Computers  - EDUCATIONAL
         * 19: Science: Math - EDUCATIONAL
         * 20: Mythology - EDUCATIONAL
         * 22: Geography - EDUCATIONAL
         * 23: History - EDUCATIONAL
         * 25: Art - EDUCATIONAL
         * 27: Animals - EDUCATIONAL
         * 
         * FOR FUN:
         * 9: General Knowledge - FOR FUN
         * 11: Film - FOR FUN
         * 12: Music - FOR FUN
         * 14: TV - FOR FUN
         * 15: Video Games - FOR FUN
         * 16: Board Games - FOR FUN
         * 21: Sports - FOR FUN
         * 28: Vehicles - FOR FUN
         * 29: Comics - FOR FUN
         * 30: Science: Gadgets - FOR FUN
         * 31: Anime/Manga - FOR FUN
         * 32: Cartoons/Animations - FOR FUN
         * 
         * 24: Politics - EXCLUDED
         * 26: Celebrities - EXCLUDED
         * 

         */
    }
}