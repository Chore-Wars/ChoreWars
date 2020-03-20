using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Chore_Wars.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Chore_Wars.Controllers
{
    public class HomeController : Controller
    {

        private readonly ChoreWarsDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(ChoreWarsDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            { 
                string aspId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var players = _context.Player.Where(x => x.PlayerStr1 == aspId).ToList();
                return View(players);
            }
            return View();
        }

        public Player sessionPlayer = new Player();
        //only when the 'login user' button is clicked
        public IActionResult LoginPlayer(int id)
        {
            sessionPlayer = _context.Player.Find(id);
            HttpContext.Session.SetString("PlayerSession", JsonConvert.SerializeObject(sessionPlayer));
            return RedirectToAction("SelectQuestion", "Question");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
