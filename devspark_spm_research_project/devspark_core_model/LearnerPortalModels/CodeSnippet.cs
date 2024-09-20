using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public class CodeSnippet
    {
        public int CodeSnippetId { get; set; }

        public int TutorialId { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
