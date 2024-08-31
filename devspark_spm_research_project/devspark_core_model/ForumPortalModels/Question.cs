using devspark_core_model.SystemModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.ForumPortalModels
{
    public class Question
    {
        public int QuestionId { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        public string UserId { get; set; }
        public DateTime DatePosted { get; set; }
        public User User { get; set; }

        public ICollection<Answer> Answers { get; set; }
         
        
    }
}
