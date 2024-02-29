using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WEB_API_DAY01.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string MgrName { get; set; }
        //public DateTime OpenDate { get; set; }
        public virtual ICollection<Student>? students { get; set; }
    }
}
