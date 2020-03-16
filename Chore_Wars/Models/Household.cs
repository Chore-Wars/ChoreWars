using System;
using System.Collections.Generic;

namespace Chore_Wars.Models
{
    public partial class Household
    {
        public int HouseholdId { get; set; }
        public string HouseholdStr1 { get; set; }
        public string HouseholdStr2 { get; set; }
        public int? HouseholdInt1 { get; set; }
        public int? HouseholdInt2 { get; set; }
        public string AspNetUsers { get; set; }
    }
}
