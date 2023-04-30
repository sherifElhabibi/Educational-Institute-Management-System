using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignemnt.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StdId { get; set; }
        [Required]
        [StringLength(20)]
        public string StdName { get; set; }
        [Range(20, 25)]
        [Required]
        public int StdAge { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new HashSet<CourseStudent>();
        public virtual ICollection<Roles> Roles { get; set;} = new HashSet<Roles>();


    }
}
