using System.Collections.Generic;
using System.Windows;
using CourseProjectOOP.Model;
using CourseProjectOOP.id;
namespace CourseProjectOOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RefreshData();

        }
        

        public List<Project> projects = DataWorker.GetAllProjects(UserId.Id);


        

        public void RefreshData()
        {
            var p = DataWorker.GetAllProjects(UserId.Id);
            Projects.ItemsSource = DataWorker.GetAllProjects(UserId.Id);
            MyTaskProjects.ItemsSource = DataWorker.GetAllProjects(UserId.Id);
            UsersProject.ItemsSource = DataWorker.GetAllProjects(UserId.Id);
            if (p.Count > 0)
            {
                List<Task> tasks = DataWorker.GetTasks(projects[0].Id);
                Tasks.ItemsSource = tasks;
                List<Task> MyTasks = DataWorker.GetMyTasks(projects[0].Id, UserId.Id);
                MyTask.ItemsSource = MyTasks;
                List<ProjectUser> users = DataWorker.GetProjectUsers(projects[0].Id);
                Users.ItemsSource = users;
            }
            else
            {
                ProjectId.Id = 0;
            }
            Projects.SelectedIndex = 0;
            MyTaskProjects.SelectedIndex = 0;
            MyTask.SelectedIndex = 0;
            UsersProject.SelectedIndex = 0;
        }

        private void CreateProject(object sender, RoutedEventArgs e)
        {
            AddProjectWindow wnd = new AddProjectWindow();
            wnd.Show();
            Close();
        }

        private void CreateTask(object sender, RoutedEventArgs e)
        {
            if (ProjectId.Id != 0)
            {
                if(DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
                {
                    AddTaskWindow wnd = new AddTaskWindow();
                    wnd.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("У вас нету доступа!");
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали проект!");
            }
            
        }

        private void AddUserButton(object sender, RoutedEventArgs e)
        {
            if (ProjectId.Id != 0)
            {
                if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
                {
                    AddProjectUserWindow wnd = new AddProjectUserWindow();
                    wnd.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("У вас нету доступа!");
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали проект");
            }
        }

        private void Projects_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var project = Projects.SelectedItem as Project;
            if (project != null)
            {
                ProjectId.Id = project.Id;
                List<Task> tasks = DataWorker.GetTasks(ProjectId.Id);
                Tasks.ItemsSource = tasks;
            }
        }

        private void DeleteTaskButton(object sender, RoutedEventArgs e)
        {
            var task = Tasks.SelectedItem as Task;
            if (task == null)
            {
                MessageBox.Show("Вы не выбрали задачу!");
            }
            else
            {
                if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
                { 
                    DataWorker.DeleteTask(ProjectId.Id, task.Id);
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("У вас нету доступа!");
                }
            }
        }

        private void DeleteProjectButton(object sender, RoutedEventArgs e)
        {
            if(ProjectId.Id != 0)
            {
                DataWorker.DeleteProject(ProjectId.Id, UserId.Id);
                RefreshData();
            }
            else
            {
                MessageBox.Show("Вы не выбрали проект!");
            }
        }

        private void DeleteUserButton(object sender, RoutedEventArgs e)
        {
            if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            {
                DeleteUser wnd = new DeleteUser();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }

        private void MyTaskProjects_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var project = MyTaskProjects.SelectedItem as Project;
            if (project != null)
            {
                ProjectId.Id = project.Id;
                List<Task> tasks = DataWorker.GetMyTasks(ProjectId.Id, UserId.Id);
                MyTask.ItemsSource = tasks;
            }
        }

        private void UsersProject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var project = UsersProject.SelectedItem as Project;
            if (project != null)
            {
                ProjectId.Id = project.Id;
                List<ProjectUser> users = DataWorker.GetProjectUsers(ProjectId.Id);
                Users.ItemsSource = users;
            }
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void DoTaskButton(object sender, RoutedEventArgs e)
        {
            var task = Tasks.SelectedItem as Task;
            if (task == null)
            {
                MessageBox.Show("Вы не выбрали задачу!");
            }
            else
            {
                if(DataWorker.ChangeStatus(ProjectId.Id, task.Id, UserId.Id))
                {
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Вы можете выполнять только свои задачи!");
                }
            }
        }

        private void AddRole(object sender, RoutedEventArgs e)
        {
            if(DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            { 
                AddRoleWindow wnd = new AddRoleWindow();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }

        private void SetRoleButton(object sender, RoutedEventArgs e)
        {
            if (DataWorker.CheckAccess(ProjectId.Id, UserId.Id))
            {
                SetRoleWindow wnd = new SetRoleWindow();
                wnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("У вас нету доступа!");
            }
        }
    }
}
