using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectOOP.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Description {  get; set; }
        public DateTime CreatedTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime Deadline { get; set;}
        public bool IsCompleted { get; set; }
        public ProjectUser ProjectUserId { get; set; }
        public Project ProjectId { get; set; }
    }
}
