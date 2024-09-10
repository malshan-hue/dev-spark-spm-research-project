using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devspark_core_model;
using devspark_core_model.SystemModels;
using Microsoft.Graph.Models;

namespace devspark_core_model.LearnerPortalModels
{
    public class CoursePrompt
    {

        public AreaOfStudyEnum AreaOfStudyEnum { get; set; }

        public string AreaOfStudyDisplayName
        {
            get
            {
                return ModelServices.GetEnumDisplayName(AreaOfStudyEnum);
            }
        }

        public CurrentStatusEnum CurrentStatusEnum { get; set; }

        public string CurrentStatusEnumDisplayname
        {
            get
            {
                return ModelServices.GetEnumDisplayName(CurrentStatusEnum);
            }
        }

        public int YearsOfExperience { get; set; }

        public AchivingLevelEnum AchivingLevelEnum { get; set; }

        public string AchivingLevelEnumDisplayName
        {
            get
            {
                return ModelServices.GetEnumDisplayName(AchivingLevelEnum);
            }
        }

        public StudyPeriodEnum StudyPeriodEnum { get; set; }

        public string StudyPeriodDisplayName
        {
            get
            {
                return ModelServices.GetEnumDisplayName(StudyPeriodEnum);
            }
        }
    }
}
