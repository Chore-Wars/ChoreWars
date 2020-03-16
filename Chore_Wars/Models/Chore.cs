using System;
using System.Collections.Generic;

namespace Chore_Wars.Models
{
    public partial class Chore
    {
        public int ChoreId { get; set; }
        public int? UserId { get; set; }
        public string ChoreName { get; set; }
        public string ChoreDescription { get; set; }
        public int? PointValue { get; set; }
        public DateTime? DueDate { get; set; }
        public string ChoreStr1 { get; set; }
        public string ChoreStr2 { get; set; }
        public int? ChoreInt1 { get; set; }
        public int? ChoreInt2 { get; set; }
    }
}
