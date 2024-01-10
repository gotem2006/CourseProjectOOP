using CourseProjectOOP.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CourseProjectOOP.id;

namespace CourseProjectOOP
{
    /// <summary>
    /// Логика взаимодействия для AddProjectUserWindow.xaml
    /// </summary>
    public partial class AddProjectUserWindow : Window
    {
        public List<User> users = DataWorker.GetUsers();
        public List<string> names = new List<string>();
        public AddProjectUserWindow()
        {
            InitializeComponent();
            foreach (User user in users)
            {
                names.Add(user.Name);
            }
            Users.ItemsSource = names;
        }

        private void AddNewUserButton(object sender, RoutedEventArgs e)
        {
            if(Users.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя");
            }
            else
            {
                int user_id = users.Where(u => u.Name == Users.SelectedValue).Select(u => u.Id).FirstOrDefault();
                if (DataWorker.GetProjectUsers(ProjectId.Id).Where(u => u.UserId.Id == user_id).Any())
                {
                    MessageBox.Show("Пользователь уже есть в проекте!");
                }
                else
                {
                    DataWorker.AddUserToProject(ProjectId.Id, user_id, null);
                    MainWindow wnd = new MainWindow();
                    wnd.Show();
                    Close();
                }
            }
        }
    }
}
