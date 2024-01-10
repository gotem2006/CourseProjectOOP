using CourseProjectOOP.Model;
using CourseProjectOOP.id;
using System.Windows;


namespace CourseProjectOOP
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    /// 
    

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        
        private bool Checkvalidation(string name, string password)
        {
            if (name == null || name.Replace(" ", "").Length == 0 || password == null || password.Replace(" ", "").Length == 0)
            {
                return false;
            }
            return true;
        }
        private void LoginButton(object sender, RoutedEventArgs e)
        {
            if(Checkvalidation(NameBlock.Text, PasswordBlock.Password)) 
            { 
                int id = DataWorker.LoginUser(NameBlock.Text, PasswordBlock.Password);
                if (id == 0)
                {
                    MessageBox.Show("Неверный пароль!");
                }
                else
                {
                    UserId.Id = id;
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Ошибка валидации!");
            }
        }
        private void RegistrationButton(object sender, RoutedEventArgs e)
        {
            if (Checkvalidation(NameBlock.Text, PasswordBlock.Password))
            {
                if (DataWorker.CreateUser(NameBlock.Text, PasswordBlock.Password))
                {
                    LoginButton(sender, e);
                }
                else
                {
                    MessageBox.Show("Пользователь с таким именем уже существует");
                }
            }
            else
            {
                MessageBox.Show("Ощибка валидации!");
            }    
        }
    }
}
