using devspark_core_model.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public class Exercise
    {
        public int ExerciseId { get; set; }

        public int SubmoduleId { get; set; }

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
    }
}
