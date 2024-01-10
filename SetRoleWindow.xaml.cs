using CourseProjectOOP.id;
using CourseProjectOOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseProjectOOP
{
    /// <summary>
    /// Логика взаимодействия для SetRoleWindow.xaml
    /// </summary>
    public partial class SetRoleWindow : Window
    {
        public List<ProjectUser> users = DataWorker.GetProjectUsers(ProjectId.Id);

        public List<Role> roles = DataWorker.GetRoles(ProjectId.Id);
        public SetRoleWindow()
        {
            InitializeComponent();
            List<string> names = new List<string>();
            foreach (ProjectUser user in users)
            {
                names.Add(user.UserId.Name);
            }
            User.ItemsSource = names;
            List<string> RolesNames = new List<string>();
            foreach(Role role in roles)
            {
                RolesNames.Add(role.Name);
            }
            Role.ItemsSource = RolesNames;
        }

        private void SetRoleButton(object sender, RoutedEventArgs e)
        {
            if(User.SelectedItem == null || Role.SelectedItem == null)
            {
                MessageBox.Show("Ошибка валидации!");
            }
            else
            {
                int userId = users.Where(user => user.UserId.Name == User.SelectedItem).Select(user => user.Id).FirstOrDefault();
                int RoleId = roles.Where(role => role.Name == Role.SelectedItem).FirstOrDefault().Id;
                DataWorker.SetRole(userId, ProjectId.Id, RoleId);
                MainWindow wnd = new MainWindow();
                wnd.Show();
                Close();
            }
            
        }
    }
}
