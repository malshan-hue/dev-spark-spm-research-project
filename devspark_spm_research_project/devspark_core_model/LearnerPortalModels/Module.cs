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
    public class Module
    {
        public int ModuleId { get; set; }

        public int CourseId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

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

        [JsonPropertyName("submodules")]
        public List<Submodule> Submodules { get; set; }
        public Course Course { get; set; }

        #endregion
    }
}
