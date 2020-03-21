using System;
using System.Collections.Generic;
using System.Linq;

namespace Chore_Wars.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int? HouseholdId { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public string Question1 { get; set; }
        public int? PointValue { get; set; }
        public string CorrectAnswer { get; set; }
        public string IncorrectAnswer1 { get; set; }
        public string IncorrectAnswer2 { get; set; }
        public string IncorrectAnswer3 { get; set; }
        public string QuestionStr1 { get; set; }
        public string QuestionStr2 { get; set; }
        public int? QuestionInt1 { get; set; }
        public int? QuestionInt2 { get; set; }

    }
}
