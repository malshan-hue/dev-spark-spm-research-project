using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public class Tutorial
    {
        public int TutorialId { get; set; }

        public int SubmoduleId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("codeSnippets")]
        public List<CodeSnippet> CodeSnippets { get; set; }
    }
}
