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
    /// Логика взаимодействия для AddRoleWindow.xaml
    /// </summary>
    public partial class AddRoleWindow : Window
    {
        public AddRoleWindow()
        {
            InitializeComponent();
        }

        private void AddRoleButton(object sender, RoutedEventArgs e)
        {
            DataWorker.AddRole(RoleBlock.Text, ProjectId.Id);
            MainWindow wnd = new MainWindow();
            wnd.Show();
            Close();
        }
    }
}
