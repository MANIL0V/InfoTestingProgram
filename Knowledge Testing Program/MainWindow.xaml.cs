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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Knowledge_Testing_Program
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            string Login = BoxLogin.Text.Trim();
            string Password = BoxPassword.Password.Trim();
            string Password2 = BoxPassword2.Password.Trim();

            if(Login.Length < 5)
            {
                BoxLogin.ToolTip = "Логин должен состоять не менее 5 символов!";
                BoxLogin.Background = Brushes.DarkMagenta;
            }
            else if(Password.Length < 5){

                BoxPassword.ToolTip = "Пароль должен состоять не менее 5 символов!";
                BoxPassword.Background = Brushes.DarkMagenta;
            }

            else if (Password != Password2)
            {

                BoxPassword2.ToolTip = "Пароль должен состоять не менее 5 символов и совпадать!";
                BoxPassword2.Background = Brushes.DarkMagenta;
            }
            else
            {
                User user = new User(Login, Password);

                db.Users.Add(user);
                db.SaveChanges();

                Auth auth = new Auth();
                auth.Show();
                Hide();
            }
        }

        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e)
        {
            Auth auth= new Auth();
            auth.Show();
            Hide();
        }
    }
}
