using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectOOP.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project ProjectId { get; set; }
    }
}
