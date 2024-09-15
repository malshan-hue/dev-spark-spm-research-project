using devspark_core_model.SystemModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace devspark_core_model.LearnerPortalModels
{
    public class Course
    {
        public int CourseId { get; set; }

        public int UserId { get; set; }

        [JsonPropertyName("courseName")]
        public string CourseName { get; set; }

        public string CourseContent { get; set; }

        [Required(ErrorMessage = "Area of Study is required.")]
        public AreaOfStudyEnum AreaOfStudyEnum { get; set; }

        public string AreaOfStudyDisplayName
        {
            get
            {
                return ModelServices.GetEnumDisplayName(this.AreaOfStudyEnum);
            }
        }

        [Required(ErrorMessage = "Current Status is required.")]
        public CurrentStatusEnum CurrentStatusEnum { get; set; }

        public string CurrentStatusEnumDisplayname
        {
            get
            {
                return ModelServices.GetEnumDisplayName(this.CurrentStatusEnum);
            }
        }

        [Required(ErrorMessage = "Years of Experience is required.")]
        public YearsOfExperienceEnum YearsOfExperienceEnum { get; set; }

        public string YearsOfExperienceEnumDisplayname
        {
            get
            {
                return ModelServices.GetEnumDisplayName(this.YearsOfExperienceEnum);
            }
        }

        [Required(ErrorMessage = "Achieving Level is required.")]
        public AchivingLevelEnum AchivingLevelEnum { get; set; }

        public string AchivingLevelEnumDisplayName
        {
            get
            {
                return ModelServices.GetEnumDisplayName(this.AchivingLevelEnum);
            }
        }

        [Required(ErrorMessage = "Study Period is required.")]
        public StudyPeriodEnum StudyPeriodEnum { get; set; }

        public string StudyPeriodDisplayName
        {
            get
            {
                return ModelServices.GetEnumDisplayName(this.StudyPeriodEnum);
            }
        }

        public DateTime CreatedDateTime { get; set; }

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

        #region NAVIGATION PROPERTIES

        [JsonPropertyName("modules")]
        public List<Module> Modules { get; set; }

        #endregion
    }
}
