using devspark_core_model.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.ForumPortalModels
{
    public class Vote
    {
        public int VoteId { get; set; }
        public string UserId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public bool IsUpvote { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }

    }
}
