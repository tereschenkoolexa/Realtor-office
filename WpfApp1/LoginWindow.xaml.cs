﻿using System;
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
using _Class;

namespace WpfApp1
{

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

        List<User> Users = new List<User> { new Admin() { Login = "Admin", Password = "K3K4",Code="0"},
                                            new Realtor() { Login = "Realtor Vadim", Password = "11R5",Code="1"},
                                            new Realtor() { Login = "Realtor Ivan", Password = "6H70",Code="1"},
                                            new Shopper() { Login = "Denis Veremuch", Password = "17B8",Code="C60O" },
                                            new Shopper() { Login = "Vitaliu Peleh", Password = "0LI0",Code="ROWQ"},
                                            new Shopper() { Login = "Oleh Knyaz", Password = "42RT",Code="F4W1"}};

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= Users.Count; i++)
            {
                if (Users[i].Login == LoginTextBox.Text && Users[i].Password == PasswordTextBox.Text)
                {
                    MessageBox.Show($"Welcome {LoginTextBox.Text}!");
                    MenuWindow menuWindow = new MenuWindow(Users[i]);
                    this.Close();
                    menuWindow.Show();
                    break;
                }
                if (Users[i].Login == LoginTextBox.Text && Users[i].Password != PasswordTextBox.Text)
                {
                    MessageBox.Show($"The password is invalid");
                    break;
                }
                if (i == Users.Count - 1 && Users[i].Login != LoginTextBox.Text
                    && Users[i].Password != PasswordTextBox.Text)
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


    
}
