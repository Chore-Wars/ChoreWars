using Chore_Wars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chore_Wars.Support
{
    public class PlayerSupport
    {
        public static IEnumerable<Player> GetPlayers(int id)
        {
            return new List<Player> { new Player { FirstName = "Kyle" } };
        }

    }
}
