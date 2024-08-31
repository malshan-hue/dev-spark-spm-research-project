using devspark_core_model.SystemModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.ForumPortalModels
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }

        [DisplayName("Explanation")]
        public string Explanation { get; set; }
        public string UserId { get; set; }
        public DateTime DatePosted { get; set; }
        public Question Question { get; set; }
        public User User { get; set; }
         
    }
}
