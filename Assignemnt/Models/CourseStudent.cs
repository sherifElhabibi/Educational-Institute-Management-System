using Castle.Components.DictionaryAdapter;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignemnt.Models
{
    public class CourseStudent
    {
        public int CrsId { get; set; }
        public int StdId { get; set; }
        public int Degrees { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }


    }
}
