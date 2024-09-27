using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public class CourseProgress
    {
        public int CourseId { get; set; }

        // total counts
        public int TotalModuleCount { get; set; }
        public int TotalSubModuleCount { get; set; }
        public int TotalTutorialCount { get; set; }
        public int TotalExerciseCount { get; set; }

        // Not Started Counts
        public int NotStartedModuleCount { get; set; }
        public int NotStartedSubModuleCount { get; set; }
        public int NotStartedTutorialCount { get; set; }
        public int NotStartedExerciseCount { get; set; }

        // In Progress Counts
        public int InProgressModuleCount { get; set; }
        public int InProgressSubModuleCount { get; set; }
        public int InProgressTutorialCount { get; set; }
        public int InProgressExerciseCount { get; set; }

        // Completed Counts
        public int CompletedModuleCount { get; set; }
        public int CompletedSubModuleCount { get; set; }
        public int CompletedTutorialCount { get; set; }
        public int CompletedExerciseCount { get; set; }
    }
}
