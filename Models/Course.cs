using System.ComponentModel.DataAnnotations;

namespace EngineeringCollegeAPI.Models
{
    public class Course
    {
       
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        
    }
}
