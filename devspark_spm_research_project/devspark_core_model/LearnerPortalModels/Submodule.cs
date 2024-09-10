using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public class Submodule
    {
        public int SubmoduleId { get; set; }

        public int ModuleId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("tutorials")]
        public List<Tutorial> Tutorials { get; set; }

        [JsonPropertyName("exercises")]
        public List<Exercise> Exercises { get; set; }
    }
}
