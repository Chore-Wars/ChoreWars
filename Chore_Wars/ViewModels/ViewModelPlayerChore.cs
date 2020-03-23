using Chore_Wars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chore_Wars.ViewModels
{
    public class ViewModelPlayerChore
    {
        public List<Player> Players { get; set; }

        public List<Chore> Chores { get; set; }

        public Player LoggedInPlayer { get; set; }

        public string UserAssignedTo { get; set; }
    }
}
