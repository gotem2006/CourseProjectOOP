using System.Windows;
using CourseProjectOOP.Model;
using CourseProjectOOP.id;

namespace CourseProjectOOP
{
    /// <summary>
    /// Логика взаимодействия для AddProjectWindow.xaml
    /// </summary>
    public partial class AddProjectWindow : Window
    {
        public AddProjectWindow()
        {
            InitializeComponent();
        }

        private void AddProjectButton(object sender, RoutedEventArgs e)
        {
            if (NameBlock.Text == null || NameBlock.Text.Replace(" ", "").Length == 0)
            {
                MessageBox.Show("Введите название проекта!");
            }
            else
            {
                DataWorker.CreateProject(NameBlock.Text, UserId.Id);
                MainWindow wnd = new MainWindow();
                wnd.Show();
                Close();
            }
        }
    }
}
