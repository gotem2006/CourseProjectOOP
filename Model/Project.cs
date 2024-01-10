using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectOOP.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Admin { get; set; }
    }
}
