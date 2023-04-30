using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Assignemnt.Models
{
    public class Roles
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Required]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
