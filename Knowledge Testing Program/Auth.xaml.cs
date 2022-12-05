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

namespace Knowledge_Testing_Program
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string Login = BoxLogin.Text.Trim();
            string Password = BoxPassword.Password.Trim();
            if (Login.Length < 5)
            {
                BoxLogin.ToolTip = "Логин должен состоять не менее 5 символов!";
                BoxLogin.Background = Brushes.DarkMagenta;
            }
            else if (Password.Length < 5)
            {

                BoxPassword.ToolTip = "Пароль должен состоять не менее 5 символов!";
                BoxPassword.Background = Brushes.DarkMagenta;
            }
            else
            {

                User authUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(b => b.login == Login && b.password ==
                    Password).FirstOrDefault();

                }

                if (authUser != null)
                {
                    Test test = new Test();
                    test.Show();
                    Hide();

                }
                else
                    MessageBox.Show("Вы ввели что-то некорректно!");
            }
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow= new MainWindow();
            mainWindow.Show();
            Hide();
        }
    }
}
