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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void LoginTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            LoginTextBox.Clear();

        }

        private void PasswordTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            PasswordTextBox.Clear();

        }

        List<User> Users = new List<User> { new Admin() { Login = "Admin", Password = "K3K4"},
                                            new Realtor() { Login = "Realtor Vadim", Password = "11R5"}};

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= Users.Count; i++)
            {
                if (Users[i].Login == LoginTextBox.Text && Users[i].Password == PasswordTextBox.Text)
                {
                    MessageBox.Show($"Welcome {LoginTextBox.Text}!");
                    break;
                }
                if (Users[i].Login == LoginTextBox.Text && Users[i].Password != PasswordTextBox.Text)
                {
                    MessageBox.Show($"The password is invalid");
                    break;
                }
                if (i == Users.Count - 1 && Users[i].Login != LoginTextBox.Text && Users[i].Password != PasswordTextBox.Text)
                {
                    MessageBox.Show($"This user does not exist");
                    break;
                }
                if (LoginTextBox.Text == "Login")
                {
                    MessageBox.Show($"Empty login");
                    break;
                }
            }


        }
    }

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    class Admin : User
    {



    }

    class Realtor : User
    {



    }
}
