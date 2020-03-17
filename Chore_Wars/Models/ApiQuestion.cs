using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chore_Wars.Models
{
    public class ApiQuestion
    {
        public int response_code { get; set; }
        public Result[] results { get; set; }
    }
    public class Result
    {
        public string category { get; set; }
        public string type { get; set; }
        public string difficulty { get; set; }
        public int point_value { get; set;}
        public string question { get; set; }
        public string correct_answer { get; set; }
        public string[] incorrect_answers { get; set; }
        public List<string> all_answers { get; set; }
        public void ScrambleAnswers(string correct, string[] incorrect)
        {
            List<string> allAnswers = new List<string>();
            allAnswers.Add(correct);
            allAnswers.Add(incorrect[0]);
            allAnswers.Add(incorrect[1]);
            allAnswers.Add(incorrect[2]);
            var shuffledAnswers = allAnswers.OrderBy(a => Guid.NewGuid()).ToList();
            all_answers = shuffledAnswers;
        }


    }
}
