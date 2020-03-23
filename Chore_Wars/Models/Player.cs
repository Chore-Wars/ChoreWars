using System;
using System.Collections.Generic;

namespace Chore_Wars.Models
{
    public partial class Player
    {

        public int UserId { get; set; }
        public int? HouseholdId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public int? CurrentPoints { get; set; }
        public int? TotalPoints { get; set; }
        public int? CorrectAnswers { get; set; }
        public int? IncorrectAnswers { get; set; }
        public int? ChoresComplete { get; set; }
        public string PlayerStr1 { get; set; }
        public string PlayerStr2 { get; set; }
        public int? PlayerInt1 { get; set; }
        public int? PlayerInt2 { get; set; }
    
    
    }
}
