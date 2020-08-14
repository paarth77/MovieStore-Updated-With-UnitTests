using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace MovieStore
{
    class Movies
    {
        //this code will be used in all methods to access the sql connection
       SqlConnection connVideos = new SqlConnection("Data Source=DESKTOP-HCE0DE1;Initial Catalog=MovieStore;Integrated Security=True");
        //this code will be used in all methods to run sql command
        SqlCommand cmdVideos = new SqlCommand();
        //Reader is the object of reader class and will be user in some methods
        SqlDataReader readerVideos;

        String queryVideos;

        public IEnumerable enumView { get; internal set; }

       


        public DataTable ListMovies()
        { //this method is used to display all the movies in datagrid
            DataTable dt = new DataTable();
            try
            {
                cmdVideos.Connection = connVideos;
                queryVideos = "Select * from Movies";

                cmdVideos.CommandText = queryVideos;
                //connection   opened
                connVideos.Open();

                // get data stream
                readerVideos = cmdVideos.ExecuteReader();

                if (readerVideos.HasRows)
                {
                    dt.Load(readerVideos);
                }
                return dt;
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
                return null;
            }
            finally
            {
                // close reader
                if (readerVideos != null)
                {
                    readerVideos.Close();
                }

                // close connection
                if (connVideos != null)
                {
                    connVideos.Close();
                }
            }

        }



        public void AddMovies(string Rating, string Title, string Year, string Rental_Cost, string Plot, string Genre, int copies)
        {//This method is used to insert data into movie table
            try
            {
                cmdVideos.Parameters.Clear();
                cmdVideos.Connection = connVideos;



                queryVideos = "Insert into Movies(Rating, Title, Year, Rental_Cost, Plot, Genre, copies) Values( @Rating, @Title, @Year, @Rental_Cost, @Plot, @Genre, @copies)";


                cmdVideos.Parameters.AddWithValue("@Rating", Rating);
                cmdVideos.Parameters.AddWithValue("@Title", Title);
                cmdVideos.Parameters.AddWithValue("@Year", Year);
                cmdVideos.Parameters.AddWithValue("@Rental_Cost", Rental_Cost);
                cmdVideos.Parameters.AddWithValue("@Plot", Plot);
                cmdVideos.Parameters.AddWithValue("@Genre", Genre);
                cmdVideos.Parameters.AddWithValue("@copies", copies);

                cmdVideos.CommandText = queryVideos;

                //connection opened
                connVideos.Open();

                // Executed query
                cmdVideos.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (connVideos != null)
                {
                    connVideos.Close();
                }
            }
        }

        public void DeleteMovie(int MovieID)
        {// this method is used to remove data from movie table
            try
            {
                cmdVideos.Parameters.Clear();
                cmdVideos.Connection = this.connVideos ;


                //blow code is to check if the Movie is rented
                String check = "";
                check = "select Count(*) from RentedMovies where MovieId = @MovieID and Rented ='1' ";
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@MovieID", MovieID) };
                cmdVideos.Parameters.Add(parameterArray[0]);

                cmdVideos.CommandText = check;
                connVideos.Open();
                //this code will delete the movie if its not rented otherwise the else statement would work
                int count = Convert.ToInt32(cmdVideos.ExecuteScalar());
                if (count == 0)
                {
                    String k = "Delete from Movies where MovieID like @MovieID";
                    cmdVideos.CommandText = k;
                    cmdVideos.ExecuteNonQuery();
                    MessageBox.Show("Movie Deleted");
                }
                else
                {
                    //display the message if he has a movie on rent 
                    MessageBox.Show("Customer has Rented a Movie He needs to return it before u can delete the Customer ");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception" + exception.Message);
            }
            finally
            {
                if (this.connVideos != null)
                {
                    this.connVideos.Close();
                }

            }
        }

       

        public void UpdateMovie(int MovieID, string Rating, string Title, int Year, string Plot, string Genre, int copies)
        {//this method is used to update the movie 
            try
            {
                cmdVideos.Parameters.Clear();
                cmdVideos.Connection = connVideos;
                queryVideos = "Update Movies Set Rating = @Rating, Title = @Title, Year = @Year,  Plot = @Plot, Genre = @Genre, copies = @copies where MovieID like @MovieID";


                cmdVideos.Parameters.AddWithValue("@MovieID", MovieID);
                cmdVideos.Parameters.AddWithValue("@Rating", Rating);
                cmdVideos.Parameters.AddWithValue("@Title", Title);
                cmdVideos.Parameters.AddWithValue("@Year", Year);
                cmdVideos.Parameters.AddWithValue("@Plot", Plot);
                cmdVideos.Parameters.AddWithValue("@Genre", Genre);
                cmdVideos.Parameters.AddWithValue("@copies", copies);


                cmdVideos.CommandText = queryVideos;

                //connection opened
                connVideos.Open();

                // Executed query
                cmdVideos.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (connVideos != null)
                {
                    connVideos.Close();
                }
            }
        }

        
    }
}
