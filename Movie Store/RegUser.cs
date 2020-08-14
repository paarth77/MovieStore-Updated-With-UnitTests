using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieStore
{
    public class RegUser
    {
        SqlConnection connRegister = new SqlConnection("Data Source=DESKTOP-HCE0DE1;Initial Catalog=MovieStore;Integrated Security=True");

        SqlCommand cmdRegister = new SqlCommand();
        String queryRegister;

        public void usrRegister(string userName, string password)
        { // this method is used to insert user details in the user table
            try
            {
                cmdRegister.Parameters.Clear();
                cmdRegister.Connection = connRegister;

                queryRegister = "Insert into userdata (UserName, Password) Values(@user, @pass)";
                cmdRegister.Parameters.AddWithValue("@user", userName);
                cmdRegister.Parameters.AddWithValue("@pass", password);

                cmdRegister.CommandText = queryRegister;
                //connection opened
                connRegister.Open();

                // get data stream
                cmdRegister.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (connRegister != null)
                {
                    connRegister.Close();
                }
            }
        }
    }
}
