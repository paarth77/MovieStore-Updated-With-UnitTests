using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieStore
{

    public partial class Main : Window
    {
        Customer  customer = new Customer() ;
        Movies movies = new Movies();
        RentedMovies rentedMovie = new RentedMovies();
        

        public int custId;
        public int movieId;

        public Main()
        {
            InitializeComponent();
            issueDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtAddress.Text != "" && txtPhone.Text != "")
            {
               string firstNAme = txtFirstName.Text;
               string lastName = txtLastName.Text;
               string address = txtAddress.Text;
               string phone = txtPhone.Text;
               int custId = Convert.ToInt32(txtCustomerId.Text);
               customer.UpdateCustomer(custId , firstNAme, lastName, address, phone);//this code passes the variable to UpdateCustomer Method in Register Class
                dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                txtMovieId.Text = "";
                txtCustomerId.Text = "";
                txtTitle.Text = "";
                txtPlot.Text = "";
                txtGenre.Text = "";
                txtYear.Text = "";
                txtRating.Text = "";
                txtMovieId.Text = "";
                txtCopies.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
            }
            else
            {
                
                    MessageBox.Show("Enter all fields","Info");
                
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtAddress.Text != "" && txtPhone.Text != "")
            {
                customer.addCustomer( txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhone.Text);//this code passes the variable to Addcustomer method in Customer Class

                dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                txtMovieId.Text = "";
                txtCustomerId.Text = "";
                txtTitle.Text = "";
                txtPlot.Text = "";
                txtGenre.Text = "";
                txtYear.Text = "";
                txtRating.Text = "";
                txtMovieId.Text = "";
                txtCopies.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";

            }
            else
            {

                MessageBox.Show("Fill all fields of customer");

            }
        }

        private void deleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (txtCustomerId.Text != "")
            { 
            int CustID = Convert.ToInt32(txtCustomerId.Text);
            MessageBoxResult dialogResult = MessageBox.Show("Are Your Sure?",
                   "Customer", MessageBoxButton.YesNo);
            if (dialogResult.ToString() == "Yes")
            {
                customer.deleteCustomer(CustID);//this code passes the variable to DeleteCustomer method in Customer Class

                dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                txtMovieId.Text = "";
                txtCustomerId.Text = "";
                txtTitle.Text = "";
                txtPlot.Text = "";
                txtGenre.Text = "";
                txtYear.Text = "";
                txtRating.Text = "";
                txtMovieId.Text = "";
                txtCopies.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
            }
          }
            else
            {
                MessageBox.Show("Customer not selected ");
            }
    }

        private void loadCustomer(object sender, RoutedEventArgs e)
        {
            dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
        }

        private void SelectBookRow_Edit(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgCustomers.SelectedItems[0];//this code is Used to transfer values from the data grid to textbox
                txtCustomerId.Text = Convert.ToString(row["CustID"]);
                txtFirstName.Text = Convert.ToString(row["FirstName"]);
                txtLastName.Text = Convert.ToString(row["Lastname"]);
                txtAddress.Text = Convert.ToString(row["Address"]);
                txtPhone.Text = Convert.ToString(row["Phone"]);

                dgCustomers.ItemsSource = customer.listCustomers().DefaultView;

            }
            catch(Exception ex)
            {
                MessageBox.Show("No Row available to select"+ex.Source);
            }
        
        }

        private void AddMovies_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtRating.Text != "" && txtTitle.Text != "" && txtYear.Text != "" &&  txtPlot.Text != "" && txtGenre.Text != "" && txtCopies.Text != "")
            {
                int Mov_year = Convert.ToInt32(txtYear.Text);//this code is used to put the value of year text box to varibles so we can calculate the rent
                int copies = Convert.ToInt32(txtCopies.Text);
                string rent;
                if (2018 - Mov_year > 5)//this if statement checks if the movie is older that five years
                {
                    rent = "2";//if the move is older that 5 year then rent is 2
                        
                }
                else
                {
                    rent = "5";//else rent is 5 
                }

                movies.AddMovies(txtRating.Text, txtTitle.Text, txtYear.Text, rent, txtPlot.Text, txtGenre.Text, copies);//this code passes the variable to AddMovies in movie Class

                dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                txtMovieId.Text = "";
                txtCustomerId.Text = "";
                txtTitle.Text = "";
                txtPlot.Text = "";
                txtGenre.Text = "";
                txtYear.Text = "";
                txtRating.Text = "";
                txtMovieId.Text = "";
                txtCopies.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
            }
            else
            {

                MessageBox.Show("Please Fill all the Details Of Movie Form");

            }
        }

        private void UpdateMovies_Click(object sender, RoutedEventArgs e)
        {
            if (txtRating.Text != "" && txtTitle.Text != "" && txtYear.Text != "" && txtPlot.Text != "" &&
                txtGenre.Text != "" && txtCopies.Text != "")
            {
                int MovieID = Convert.ToInt32(txtMovieId.Text);
                int copies = Convert.ToInt32(txtCopies.Text);


                string Title = txtTitle.Text;
                string Rating = txtRating.Text;
                string Plot = txtPlot.Text;
                string Genre = txtGenre.Text;
                int Year = Convert.ToInt32(txtYear.Text);
                movies.UpdateMovie(MovieID, Rating, Title, Year, Plot, Genre, copies);//this code passes the variable to Updatemovie method in Register Class

                MessageBox.Show("Movie Updated successfully");
                dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                txtMovieId.Text = "";
                txtCustomerId.Text = "";
                txtTitle.Text = "";
                txtPlot.Text = "";
                txtGenre.Text = "";
                txtYear.Text = "";
                txtRating.Text = "";
                txtMovieId.Text = "";
                txtCopies.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
            }
            else
            {

                MessageBox.Show("Please Fill all the Details Of Movie Form");

            }
        }

        private void DeleteMovie(object sender, RoutedEventArgs e)
        {

            if (txtMovieId.Text != "")
            {

                


                MessageBoxResult dialogResult = MessageBox.Show("Are Your Sure You want To Delete This Movie ?",
                    "movie", MessageBoxButton.YesNo);
                if (dialogResult.ToString() == "Yes")
                {
                     int movie = Convert.ToInt32(txtMovieId.Text);
                    movies.DeleteMovie(movie);//this code passes the variable to DeleteMovie in Movie Class

                    dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                    dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                    dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                    txtMovieId.Text = "";
                    txtCustomerId.Text = "";
                    txtTitle.Text = "";
                    txtPlot.Text = "";
                    txtGenre.Text = "";
                    txtYear.Text = "";
                    txtRating.Text = "";
                    txtMovieId.Text = "";
                    txtCopies.Text = "";
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtAddress.Text = "";
                    txtPhone.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Customer Not Selected");
            }

        }

        private void movie_loaded(object sender, RoutedEventArgs e)
        {
           dgVideos.ItemsSource = movies.ListMovies().DefaultView;
        }

        private void SelectMovieRow_Edit(object sender, MouseButtonEventArgs e)
        {// below code is used to take the values od datagrid and put it in the textbox
            try
            {
                DataRowView row = (DataRowView)dgVideos.SelectedItems[0];
                txtTitle.Text = Convert.ToString(row["Title"]);
                txtPlot.Text = Convert.ToString(row["Plot"]);
                txtGenre.Text = Convert.ToString(row["Genre"]);
                txtYear.Text = Convert.ToString(row["Year"]);
                txtRating.Text = Convert.ToString(row["Rating"]);
                txtMovieId.Text = Convert.ToString(row["MovieID"]);
                txtCopies.Text = Convert.ToString(row["copies"]);

                dgVideos.ItemsSource = movies.ListMovies().DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show("No row available to select"+ex.Source);
            }
           
        }

       

        

        private void Returned_Click(object sender, RoutedEventArgs e)
        {
            if (txtRentedMovieId.Text != "")
            {
                int RMID = Convert.ToInt32(txtRentedMovieId.Text);
                int MovieID = Convert.ToInt32(txtMovieId.Text);



                rentedMovie.updateRentedMovie(RMID, MovieID, Convert.ToDateTime(issueDate.Text), DateTime.Now);//this code passes the variable to UpdateRented Method in Rented Class

                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                txtMovieId.Text = "";
                txtCustomerId.Text = "";
                txtTitle.Text = "";
                txtPlot.Text = "";
                txtGenre.Text = "";
                txtYear.Text = "";
                txtRating.Text = "";
                txtMovieId.Text = "";
                txtCopies.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
            }
            else
            {
                MessageBox.Show("can not return a movie, please select first ");

            }

        }
        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            if (txtMovieId.Text != "" && txtCustomerId.Text != "" && issueDate.Text != "")
            {
                if (txtCopies.Text != "0")

                {

                    int MovieID = Convert.ToInt32(txtMovieId.Text);
                    int Customerid = Convert.ToInt32(txtCustomerId.Text);
                    issueDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    int copies = Convert.ToInt32(txtCopies.Text);
                    int isout = 1;

                    //this code passes the variable to Addrented Method in rented Class
                    rentedMovie.addRentedMovie(MovieID, Customerid, DateTime.Now, copies, isout);
                    dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                    dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                    dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                    txtMovieId.Text = "";
                    txtCustomerId.Text = "";
                    txtTitle.Text = "";
                    txtPlot.Text = "";
                    txtGenre.Text = "";
                    txtYear.Text = "";
                    txtRating.Text = "";
                    txtMovieId.Text = "";
                    txtCopies.Text = "";
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtAddress.Text = "";
                    txtPhone.Text = "";

                }


                else
                {
                    MessageBox.Show("no  more copy of this movie available");
                }
            }
            else
            {
                MessageBox.Show("To Rent a Movie: Select Customer First and then select a movie");
            }
        }

        

        private void selectRentMovie(object sender, MouseButtonEventArgs e)
        { 
            try
            {
                //below code is used to put data from DataGrid in Textbox
                DataRowView row = (DataRowView)dgRented.SelectedItems[0];
                txtMovieId.Text = Convert.ToString(row["MovieId"]);
                txtCustomerId.Text = Convert.ToString(row["CustomerId"]);
                txtRentedMovieId.Text = Convert.ToString(row["RentMovieID"]);
                issueDate.Text = Convert.ToString(row["DateOfRented"]);
                txtDateReturned.Text = DateTime.Now.ToString("dd-MM-yyyy");



                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Rows are not available"+ ex.Message);
            }
           
        }

        private void loadVideo(object sender, RoutedEventArgs e)
        {
           
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;

        }

        private void rented(object sender, RoutedEventArgs e)
        {
           
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if(txtRentedMovieId.Text != "")
            { 
                int RMID = Convert.ToInt32(txtRentedMovieId.Text);
                txtDateReturned.Text = DateTime.Today.ToString("dd-MM-yyyy");
                int MovieID = Convert.ToInt32(txtMovieId.Text);


                //this code passes the variable to UpdateMovie in Rented Class
                rentedMovie.updateRentedMovie(RMID, MovieID, Convert.ToDateTime(issueDate.Text), DateTime.Now);

                dgVideos.ItemsSource = movies.ListMovies().DefaultView;
                dgRented.ItemsSource = rentedMovie.getListRentedMovies().DefaultView;
                dgCustomers.ItemsSource = customer.listCustomers().DefaultView;
                txtMovieId.Text = "";
                txtCustomerId.Text = "";
                txtTitle.Text = "";
                txtPlot.Text = "";
                txtGenre.Text = "";
                txtYear.Text = "";
                txtRating.Text = "";
                txtMovieId.Text = "";
                txtCopies.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
            }
            else
            {
                MessageBox.Show("Select a movie by double clicking on table row");
            }

        }

        private void btnTopCustomer_Click(object sender, RoutedEventArgs e)
        {
            rentedMovie.getTopCustomer();//this code passes the variable to TopCustomer Method in Rented Class
        }

        private void btnTopMovie_Click(object sender, RoutedEventArgs e)
        {   //this code passes the variable to TopMovie Method in Rented Class
            rentedMovie.getTopMovie();
        }

    }
}
