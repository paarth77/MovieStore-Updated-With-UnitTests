using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieStore
{
    public class AuthUser
    {
        SqlConnection connLogin = new SqlConnection("Data Source=DESKTOP-HCE0DE1;Initial Catalog=MovieStore;Integrated Security=True");

        SqlCommand cmdLogin = new SqlCommand();

        SqlDataReader readerLogin;

        String queryLogin;
        public bool TestDBConnection
        {
            get
            {
                if(connLogin.State==ConnectionState.Closed)
                {
                    connLogin.Open();
                }
                return true;
            }
                
        }

        public bool login(string username, string password)
        { //this method is used to check if the enter data exist in the user table s
            try
            {
                cmdLogin.Connection = connLogin;

                queryLogin = "Select username, password from userdata where UserName =  @UserName  and Password =  @password ";

             
                cmdLogin.Parameters.AddWithValue("@UserName", username);
                cmdLogin.Parameters.AddWithValue("@password", password);

                cmdLogin.CommandText = queryLogin;
                connLogin.Open();

                readerLogin = cmdLogin.ExecuteReader();

                if (readerLogin.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                return false;
            }
            finally
            {
                if (readerLogin != null)
                {
                    readerLogin.Close();
                }

                if (connLogin != null)
                {
                    connLogin.Close();
                }
            }
        }

    }
}
