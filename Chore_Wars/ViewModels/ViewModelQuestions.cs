using Chore_Wars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chore_Wars.ViewModels
{
    public class ViewModelQuestions
    {
        public ApiQuestion ApiQuestion { get; set; }
        public Question CustomQuestion { get; set; }

        public ViewModelQuestions()
        {

        }
        public ViewModelQuestions(Question customQuestion)
        {
            CustomQuestion = customQuestion;
        }
    }
}
