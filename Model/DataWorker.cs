using CourseProjectOOP.id;
using CourseProjectOOP.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectOOP.Model
{
    public class DataWorker
    {
        #region user's query
        public static bool CreateUser(string name, string password)
        {
            using (Context db = new Context())
            {
                if (db.Users.Any(user => user.Name == name))
                {
                    return false;
                    
                }
                User user = new User
                {
                    Name = name,
                    Password = password
                };
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
        }

        

        public static int LoginUser(string name, string password)
        {
            using (Context db = new Context())
            {
                if(db.Users.Any(user => user.Name == name && user.Password == password))
                {
                    return db.Users.First(user => user.Name == name && user.Password == password).Id;
                }
                return 0;
            }
        }


        public static List<User> GetUsers()
        {
            using (Context db = new Context())
            {
                List<User> users = db.Users.ToList();
                return users;
            }
        }
        #endregion

        #region project's query
        public static string CreateProject(string name, int user_id)
        {
            using(Context db = new Context())
            {
                User user = db.Users.First(admin => admin.Id == user_id);
                Project project = new Project
                {
                    Name = name,
                    Admin = user
                };

                db.Projects.Add(project);
                db.SaveChanges();
                ProjectUser projectUser = new ProjectUser
                {
                    UserId = user,
                    ProjectId = project,
                    RoleId = null
                };
                db.ProjectUsers.Add(projectUser);
                db.SaveChanges();
                return "";
            }
        }

        public static void DeleteProject(int projectId, int user_id)
        {
            using (Context db = new Context())
            {
                Project project = db.Projects.Include(p => p.Admin).First(p => p.Id == projectId);
                if (user_id == project.Admin.Id)
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                }
                else
                {
                    ProjectUser user = db.ProjectUsers.First(u => u.ProjectId == project && u.UserId.Id == user_id);
                    db.ProjectUsers.Remove(user); 
                    db.SaveChanges();
                }
            }
        }
        public static List<Project> GetAllProjects(int user_id)
        {
            using(Context db = new Context())
            {
                List<ProjectUser> ProjectUsers = db.ProjectUsers.Include(p => p.UserId).Include(p => p.ProjectId).Include(p => p.ProjectId.Admin).Where(p=> p.UserId.Id == user_id).ToList();
                List<Project> UserProjects = new List<Project>();
                
                foreach(var project in ProjectUsers)
                {
                    UserProjects.Add(project.ProjectId);
                }
                return UserProjects;
            }
        }

        #endregion

        #region role's query
        public static string AddRole(string name, int project_id)
        {
            using (Context db = new Context())
            {
                Project project = db.Projects.First(p => p.Id == project_id);
                Role role = new Role
                {
                    Name = name,
                    ProjectId = project

                };
                db.Roles.Add(role);
                db.SaveChanges();
            }
            return "Роль создана";
        }

        public static void SetRole(int user_id, int projectId, int roleId)
        {
            using (Context db = new Context())
            {
                ProjectUser user = db.ProjectUsers.First(u => u.Id == user_id && u.ProjectId.Id == projectId);
                Role role = db.Roles.First(r => r.Id == roleId);
                user.RoleId = role;
                db.SaveChanges();
            }
        }

        public static List<Role> GetRoles(int project_id)
        {
            using(Context db = new Context())
            {
                return db.Roles.Where(r => r.ProjectId.Id == project_id).ToList();
            }
        }



        #endregion

        #region ProjectUser's query
        public static string AddUserToProject(int project_id, int user_id, int? role_id)
        {
            using (Context db = new Context())
            {
                User user = db.Users.First(u => u.Id == user_id);
                Project project = db.Projects.First(p => p.Id == project_id);
                if(role_id != null)
                {
                    Role role = db.Roles.First(r => r.Id == role_id);
                    ProjectUser projectUser = new ProjectUser
                    {
                        ProjectId = project,
                        UserId = user,
                        RoleId = role
                    };
                    db.ProjectUsers.Add(projectUser);
                    db.SaveChanges();
                }
                else
                {
                    ProjectUser projectUser = new ProjectUser
                    {
                        ProjectId = project,
                        UserId = user,
                        RoleId = null
                    };
                    db.ProjectUsers.Add(projectUser);
                    db.SaveChanges();
                }
                
            }
            return "Пользователь добавлен";
        }

        public static bool DeleteProjectUser(int project_userId, int projectId)
        {
            using (Context db = new Context())
            {
                ProjectUser user = db.ProjectUsers.Include(u => u.UserId).First(u => u.ProjectId.Id == projectId && u.Id == project_userId);
                Project admin = db.Projects.FirstOrDefault(p => p.Admin.Id == user.UserId.Id && p.Id == projectId);
                if(admin != null)
                {
                    if (admin.Admin.Id != user.UserId.Id)
                    {
                        db.ProjectUsers.Remove(user);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    db.ProjectUsers.Remove(user);
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public static List<ProjectUser> GetProjectUsers(int projectId)
        {
            using (Context db = new Context())
            {
                List<ProjectUser> users = db.ProjectUsers.Include(u => u.ProjectId).Include(u => u.UserId).Include(u => u.RoleId).Where(u => u.ProjectId.Id == projectId).ToList();
                return users;
            }
        }
        #endregion

        #region task's query
        public static string AddTask(string description, DateTime DeadLine, int projectId, int ProjectUserId)
        {
            using (Context db = new Context())
            {
                Project project = db.Projects.First(p => p.Id == projectId);
                ProjectUser user = db.ProjectUsers.First(u => u.Id == ProjectUserId && u.ProjectId == project);
                Task task = new Task
                {
                    Description = description,
                    CreatedTime = DateTime.UtcNow,
                    Deadline = DeadLine,
                    ProjectId = project,
                    ProjectUserId = user,
                    IsCompleted = false

                };
                db.Tasks.Add(task);
                db.SaveChanges();
            }
            return "";
        }

        public static bool DeleteTask(int  projectId, int? taskId)
        {
            if (taskId == null) return false;
            using(Context db = new Context())
            {
                Task task = db.Tasks.First(p => p.Id == taskId && p.ProjectId.Id == projectId);
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
            return true;
        }
        public static List<Task> GetTasks(int projectId)
        {
            using (Context db = new Context())
            {
                List<Task> tasks = db.Tasks.Include(t => t.ProjectId).Include(t=> t.ProjectUserId).Include(t=> t.ProjectUserId.UserId).Where(t => t.ProjectId.Id == projectId).ToList();
                return tasks;
            }
            
        }

        public static List<Task> GetMyTasks(int projectId, int userId)
        {
            using(Context db = new Context())
            {
                return db.Tasks.Include(t => t.ProjectId).Include(t => t.ProjectUserId).Include(t => t.ProjectUserId.UserId).Where(t => t.ProjectId.Id == projectId && t.ProjectUserId.UserId.Id == userId).ToList();
            }
        }
        
        public static bool ChangeStatus(int project_id, int task_id, int user_id)
        {
            using(Context db = new Context())
            {
                
                Task task = db.Tasks.FirstOrDefault(p => p.Id == task_id && p.ProjectId.Id == project_id && p.ProjectUserId.UserId.Id == user_id);
                if(task == null)
                {
                    return false;
                }
                task.IsCompleted = true;
                db.SaveChanges();
                return true;
            }
        }
        #endregion


        public static bool CheckAccess(int projectId, int userId)
        {
            using(Context db = new Context())
            {
                Project project = db.Projects.FirstOrDefault(p => p.Admin.Id == userId);
                if (project == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
