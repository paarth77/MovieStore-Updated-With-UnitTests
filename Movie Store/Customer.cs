using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using System.Windows;

namespace MovieStore
{
   public class Customer
    {    
        
        //this code will be used in all methods to access the sql connection
        SqlConnection connCustomer = new SqlConnection("Data Source=DESKTOP-HCE0DE1;Initial Catalog=MovieStore;Integrated Security=True");
        //this code will be used in all methods to run sql command
        SqlCommand cmdCustomer = new SqlCommand();
        //Reader is the object of reader class and will be user in some methods
        SqlDataReader readerCustomer;
        String queryCustomer;

        public IEnumerable enumView { get; internal set; }

        internal object CustomerDG()
        {
            throw new NotImplementedException();
        }

     
        public DataTable listCustomers()
        {
            DataTable  dt = new DataTable();
            try
            {// this method is used to display customers on data grid
                cmdCustomer.Connection = connCustomer;
                queryCustomer = "Select * from Coustmer";

                cmdCustomer.CommandText = queryCustomer;
                connCustomer.Open();

                readerCustomer = cmdCustomer.ExecuteReader();

                if (readerCustomer.HasRows)
                {
                    dt.Load(readerCustomer);
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception" + ex.Message);
                return null;
            }
            finally
            {
                if (readerCustomer != null)
                {
                    readerCustomer.Close();
                }

                if (connCustomer != null)
                {
                    connCustomer.Close();
                }
            }

        }


        
        public void addCustomer(string firstName, string lastName, string address, string phone)
        {// this method is used to add customer
            try
            {
                cmdCustomer.Parameters.Clear();
                cmdCustomer.Connection = connCustomer;

                

                queryCustomer = "Insert into Coustmer(FirstName, LastName, Address, Phone) Values( @FirstName, @LastName, @Address, @Phone)";

                
                cmdCustomer.Parameters.AddWithValue("@FirstName", firstName );
                cmdCustomer.Parameters.AddWithValue("@LastName", lastName );
                cmdCustomer.Parameters.AddWithValue("@Address", address );
                cmdCustomer.Parameters.AddWithValue("@Phone", phone );

                cmdCustomer.CommandText = queryCustomer;

                //connection opened
                connCustomer.Open();

                // Executed query
                cmdCustomer.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (connCustomer != null)
                {
                    connCustomer.Close();
                }
            }
        }

        public void deleteCustomer(Int32 custId)
        {//this method is used to delete customer
            try
            {
                cmdCustomer .Parameters.Clear();
                cmdCustomer.Connection = this.connCustomer;

                //below code is to find if the customer has rented a movie 
                String Strr = "";
                Strr = "select Count(*) from RentedMovies where CustomerId= @CustID";
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@CustID", custId) };
                cmdCustomer.Parameters.Add(parameterArray[0]);

                cmdCustomer.CommandText = Strr;
                connCustomer .Open();
                int count = Convert.ToInt32(cmdCustomer.ExecuteScalar());
                if (count == 0)//if customer has not rented the movie it will be deleted if not then the customer first have to return the movie 
                {
                    Strr = "Delete from Coustmer where CustID like @CustID";
                    cmdCustomer.CommandText = Strr;
                    cmdCustomer.ExecuteNonQuery();
                    MessageBox.Show("User Deleted sucessfully");
                }
                else
                {
                    MessageBox.Show("Customer has rented a movie so can not delete. Return movie first");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception" + exception.Message);
            }
            finally
            {
                if (this.connCustomer != null)
                {
                    this.connCustomer.Close();
                }
            }
        }

    

    public void UpdateCustomer(int custId, string firstName, string lastName, string address, string phone)
        {//This method is Used to update customer table
            try
            {
                cmdCustomer.Parameters.Clear();
                cmdCustomer.Connection = connCustomer;
                queryCustomer = "Update Coustmer Set FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone where CustID = @CustID";


                cmdCustomer.Parameters.AddWithValue("@CustID", custId);
                cmdCustomer.Parameters.AddWithValue("@FirstName", firstName);
                cmdCustomer.Parameters.AddWithValue("@LastName", lastName );
                cmdCustomer.Parameters.AddWithValue("@Address", address);
                cmdCustomer.Parameters.AddWithValue("@Phone", phone);

                cmdCustomer.CommandText = queryCustomer;

                //connection opened
                connCustomer.Open();

                // Executed query
                cmdCustomer.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (connCustomer != null)
                {
                    connCustomer.Close();
                }
            }
        }

    }
}   

