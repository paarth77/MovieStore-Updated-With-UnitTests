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

namespace MovieStore
{
 
    public partial class LoginForm : Window
    {
        AuthUser login = new AuthUser();
        public LoginForm()
        { 
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

      
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            (new RegisterForm()).Show();
            this.Close();
        }

        private void Login_click(object sender, RoutedEventArgs e)
        {
            if (login.login(txtUserName.Text, txtPassword.Text))
                //this code passes the variable to 'login' in Login Class 

            {
                MessageBox.Show("Login Successfully");
                (new Main()).Show();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("Username or Password is invalid");
                (new LoginForm()).Show();
                this.Close();
            }

        }
    }
}
