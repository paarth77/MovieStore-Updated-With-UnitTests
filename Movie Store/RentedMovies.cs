using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using System.Windows;

namespace MovieStore
{
    class RentedMovies
    {

        public IEnumerable enumView { get; internal set; }
        public string queryStrings { get; private set; }
        public string queryString2 { get; private set; }
        //this code will be used in all methods to access the sql connection



          
        SqlConnection connRentMovie = new SqlConnection("Data Source=DESKTOP-HCE0DE1;Initial Catalog=MovieStore;Integrated Security=True");
        //will be used in all methods to run sql command

        SqlCommand cmdRentMovie = new SqlCommand();

        SqlDataReader readerRentMovie;

        String queryRentMovie;

       

        internal object RentedDG()
        {
            throw new NotImplementedException();
        }


        public DataTable getListRentedMovies()
        {
            DataTable dt = new DataTable();
            try
            {
                cmdRentMovie.Connection = connRentMovie;
                queryRentMovie = "Select * from RentedMovies Order by RentMovieID DESC";

                cmdRentMovie.CommandText = queryRentMovie;
                //connection   opened
                connRentMovie.Open();

                // get data stream
                readerRentMovie = cmdRentMovie.ExecuteReader();

                if (readerRentMovie.HasRows)
                {
                    dt.Load(readerRentMovie);
                }
                return dt;
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception " + ex.Message);
                return dt;
            }
            finally
            {
                // close reader
                if (readerRentMovie  != null)
                {
                    readerRentMovie.Close();
                }

                // close connection
                if (connRentMovie != null)
                {
                    connRentMovie.Close();
                }
            }

        }



        public void addRentedMovie(int movieId, int customerId, DateTime  dateOfRented, int noOfCopies, int Rented)
        {// thsi code is used to issue movie 
            try
            {
                cmdRentMovie.Parameters.Clear();
                cmdRentMovie.Connection = connRentMovie;



                queryRentMovie = "Insert into RentedMovies(MovieId, CustomerId, DateOfRented ,Rented) Values( @MovieID, @CustID, @DateRented, @Rented)";
                
                cmdRentMovie.Parameters.AddWithValue("@MovieID", movieId );
                cmdRentMovie.Parameters.AddWithValue("@CustID", customerId );
                cmdRentMovie.Parameters.AddWithValue("@DateRented", dateOfRented );
                cmdRentMovie.Parameters.AddWithValue("@Rented", Rented);
                cmdRentMovie.Parameters.AddWithValue("@copies", noOfCopies);


                cmdRentMovie.CommandText = queryRentMovie;

                //connection opened
                connRentMovie.Open();

                // Executed query
                cmdRentMovie.ExecuteNonQuery();

                queryRentMovie = "Update Movies set copies = copies-1 where MovieID = @MovieID";
                cmdRentMovie.CommandText = queryRentMovie;
                cmdRentMovie.ExecuteNonQuery();
                

               
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (connRentMovie != null)
                {
                    connRentMovie.Close();
                }
            }
        }


        public void updateRentedMovie(int RMID, int MovieID, DateTime  DateRent, DateTime  DateReturned)
        {// this method is used to return the movie 
            try
            {
                cmdRentMovie.Parameters.Clear();
                cmdRentMovie.Connection = connRentMovie;
                int RentTotal = 0, Cost = 0;
                double days = (DateReturned - DateRent).TotalDays;

                string S1 = "Select Rental_Cost from Movies where MovieID = @MovieID";
                cmdRentMovie.Parameters.AddWithValue("@MovieID", MovieID);

                cmdRentMovie.CommandText = S1;
                connRentMovie.Open();
                Cost = Convert.ToInt32(cmdRentMovie.ExecuteScalar());

                if (Convert.ToInt32(days) == 0)
                {
                    RentTotal = Cost;
                }
                else
                {
                    RentTotal = Cost * Convert.ToInt32(days);
                }


                queryStrings = "Update RentedMovies Set DateOfReturned=@DateReturned where RentMovieID = @RMID";
                cmdRentMovie.Parameters.AddWithValue("@DateReturned", DateReturned);
                cmdRentMovie.Parameters.AddWithValue("@RMID", RMID);
               
                cmdRentMovie.CommandText = queryStrings;

                cmdRentMovie.ExecuteNonQuery();


                queryStrings = "Update Movies set copies = copies+1 where MovieID = @MovieID";
                this.cmdRentMovie.CommandText = this.queryStrings;

                this.cmdRentMovie.ExecuteNonQuery();

                queryStrings = "Update RentedMovies set Rented = 0 where RentMovieID = @RMID";
                this.cmdRentMovie.CommandText = this.queryStrings;

                this.cmdRentMovie.ExecuteNonQuery();

                MessageBox.Show("Total Rent is " + RentTotal);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception " + exception.Message);
            }
            finally
            {
                if (connRentMovie != null)
                {
                    connRentMovie.Close();
                }
            }


        }

        public void getTopCustomer()
        {// this mehod is used to find the top Custometr
            int Top = 0, Max = 0, Total = 0;
            string Value = "";
            try
            {
                cmdRentMovie.Parameters.Clear();
                cmdRentMovie.Connection = connRentMovie;
                string Val = "Select IDENT_CURRENT('Coustmer')";

                cmdRentMovie.CommandText = Val;
                connRentMovie.Open();
                Total = Convert.ToInt32(cmdRentMovie.ExecuteScalar());

                for (int i = 1; i <= Total; i++)
                {

                    Value = "select Count(*) from RentedMovies where CustomerId= '" + i + "'";


                    cmdRentMovie .CommandText = Value;
                    int count = Convert.ToInt32(cmdRentMovie.ExecuteScalar());
                    if (count > Max)
                    {
                        Max = count;
                        Top = i;
                    }
                }
                this.queryStrings = "Select FirstName from Coustmer where CustID ='" + Top + "'";
                this.cmdRentMovie.CommandText = this.queryStrings;
                String FirstName = Convert.ToString(cmdRentMovie.ExecuteScalar());
                MessageBox.Show("Top Customer: "+FirstName + "\nRented Movies: " + Max);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception " + exception.Message);
            }
            finally
            {
                if (connRentMovie != null)
                {
                    connRentMovie.Close();
                }
            }

        }


        public void getTopMovie()
        {// this method is used to display the top movie 
            int Top = 0, Max = 0, Total = 0;
            string Value = "";
            try
            {
                cmdRentMovie .Parameters.Clear();
                cmdRentMovie.Connection = connRentMovie; 
                string Val = "Select IDENT_CURRENT('Movies')";

                cmdRentMovie.CommandText = Val;
                connRentMovie.Open();
                Total = Convert.ToInt32(cmdRentMovie.ExecuteScalar());

                for (int i = 1; i <= Total; i++)
                {

                    Value = "select Count(*) from RentedMovies where MovieID= '" + i + "'";


                    cmdRentMovie.CommandText = Value;
                    int count = Convert.ToInt32(cmdRentMovie.ExecuteScalar());
                    if (count > Max)
                    {
                        Max = count;
                        Top = i;
                    }
                }

                
                this.queryString2= "Select Title from Movies where MovieID ='" + Top + "'";
                this.cmdRentMovie.CommandText = this.queryString2;
                String Title = Convert.ToString(cmdRentMovie.ExecuteScalar());
                MessageBox.Show("Top Movie: "+Title + "\nRented: " + Max);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception " + exception.Message);
            }
            finally
            {
                if (connRentMovie != null)
                {
                    connRentMovie.Close();
                }
            }

        }
    }
}

