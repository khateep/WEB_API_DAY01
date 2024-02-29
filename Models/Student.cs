using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WEB_API_DAY01.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        
        public virtual Department? Department { get; set; }

    }
}
