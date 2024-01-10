using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectOOP.Model
{
    public class ProjectUser
    {
        public int Id { get; set; }
        public Role? RoleId { get; set; }
        public User UserId { get; set; }
        public Project ProjectId { get; set; }
    }
}
