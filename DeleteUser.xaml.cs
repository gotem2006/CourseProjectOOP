using CourseProjectOOP.Model;
using System.Windows;
using CourseProjectOOP.id;
using System.Collections.Generic;
using System.Linq;

namespace CourseProjectOOP
{
    /// <summary>
    /// Логика взаимодействия для DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : Window
    {

        public List<ProjectUser> users = DataWorker.GetProjectUsers(ProjectId.Id);
        public List<string> names = new List<string>();
        public DeleteUser()
        {
            InitializeComponent();
            foreach (ProjectUser user in users)
            {
                names.Add(user.UserId.Name);
            }
            Users.ItemsSource = names;
        }

        private void DeleteUserButton(object sender, RoutedEventArgs e)
        {
            if (Users.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя!");
            }
            else
            {
                int user_id = users.Where(u => u.UserId.Name == Users.SelectedValue).First().Id;
                if(DataWorker.DeleteProjectUser(user_id, ProjectId.Id))
                {
                    MainWindow wnd = new MainWindow();
                    wnd.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Пользователь не может быть удален!");
                }
            }
        }
    }
}
