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
   
    public partial class RegisterForm : Window
    {
        RegUser  register= new RegUser();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {   //below code is used to Check if the user has fill both the column
            if (txtUserName.Text != "" && txtPassword.Text != "")
            {
                string userName = Convert.ToString(txtUserName.Text);//this code takes the value from the text box and put it in the variable 
                string password = Convert.ToString(txtPassword.Text);
                register.usrRegister(userName, password);//this code passes the variable to Regis_method in Register Class

                MessageBox.Show("Registered Successfully");//this code display to the user by a pop up that they have been register successfully
                LoginForm w = new LoginForm();
                w.ShowDialog();//this code display the login window
                this.Close();
            }

            else
            {
                MessageBox.Show("Enter Username and Password");// this code display to the user by a pop up that they Does not filled the both column

            }

        }
    }
}
