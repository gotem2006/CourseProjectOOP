using CourseProjectOOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CourseProjectOOP.id;

namespace CourseProjectOOP
{
    /// <summary>
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {



        public List<ProjectUser> users = DataWorker.GetProjectUsers(ProjectId.Id);


        public AddTaskWindow()
        {
            InitializeComponent();
            //List<ProjectUser> users = DataWorker.GetProjectUsers(ProjectId.Id);
            List<string> names = new List<string>();
            foreach (ProjectUser user in users)
            {
                names.Add(user.UserId.Name);
            }
            User.ItemsSource = names;

        }

        private void CreateTask(object sender, RoutedEventArgs e)
        {
            if(Description.Text == null || Description.Text.Replace(" ", "").Length == 0 || Deadline.SelectedDate.HasValue == false || User.SelectedItem == null) 
            {
                MessageBox.Show("Ошибка валидации!");
            }
            else
            {
                int userId = users.Where(user => user.UserId.Name == User.SelectedItem).Select(user => user.Id).FirstOrDefault();
                DateTime deadline = Deadline.SelectedDate.Value;
                DataWorker.AddTask(Description.Text, deadline, ProjectId.Id, userId);
                MainWindow wnd = new MainWindow();
                wnd.Show();
                Close();
            }
        }
    }
}
