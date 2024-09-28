using devspark_core_model.SystemModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public ProgressStatusEnum ProgressStatusEnum { get; set; }

        public string ProgressStatusEnumDisplayName
        {
            get
            {
                return ModelServices.GetEnumDisplayName(this.ProgressStatusEnum);
            }
        }

        [NotMapped]
        public string EncryptedKey { get; set; }

        #region NAVIGATIONAL PROPERTIES

        [JsonPropertyName("tutorials")]
        public List<Tutorial> Tutorials { get; set; }

        [JsonPropertyName("exercises")]
        public List<Exercise> Exercises { get; set; }
        public Module Module { get; set; }

        #endregion
    }
}
