using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace GamePlanet
{
    public class DBLogin
    {

        // FOR USER CONTROL

        public static int MySQLLogin(string username, string password)
        {

            try
            {
                string connStr = GetConnectionString();
                //string sql = string.Format("SELECT UserName FROM User HAVING UserName={0}", user);
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(@"SELECT COUNT(*) FROM User WHERE UserName=@uname and Password=@pass", conn);
                    cmd.Parameters.AddWithValue("@uname", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    // Check if login successful

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result > 0)
                    {
                        MessageBox.Show("Login Success");
                        conn.Close();
                        return result;
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is incorrect");
                        conn.Close();
                        return result;
                    }
                }


            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return 0;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }


        }

        public static void MySQLCreate(string firstname, string lastname, string username, string email, string password)
        {

            try
            {

                string connStr = GetConnectionString();
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {

                    conn.Open();
                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "INSERT INTO User(FirstName, LastName, Username, Email, Description, Password, CreatedAt) VALUES(@fname, @lname, @uname, @email, @desc, @pword, @cat)";
                    comm.Parameters.AddWithValue("@fname", firstname);
                    comm.Parameters.AddWithValue("@lname", lastname);
                    comm.Parameters.AddWithValue("@uname", username);
                    comm.Parameters.AddWithValue("@email", email);
                    comm.Parameters.AddWithValue("@desc", "Test description");
                    comm.Parameters.AddWithValue("@pword", password);
                    comm.Parameters.AddWithValue("@cat", "NOW()");
                    comm.ExecuteNonQuery();
                    conn.Close();

                    // Check if create user succeeded

                }


            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        public static User MySQLGetProfile(string username)
        {
            try
            {
                string connStr = GetConnectionString();
                User account = new User();

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // Command to get profile
                    MySqlCommand comm1 = conn.CreateCommand();
                    comm1.CommandText = "SELECT UserID, FirstName, LastName, UserName, Email, AvatarPath, Description FROM User WHERE UserName = '" + username + "'";

                    using (MySqlDataReader reader = comm1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account.UserID = reader.GetInt32(0);
                            account.Firstname = reader.GetString(1);
                            account.Lastname = reader.GetString(2);
                            account.Username = reader.GetString(3);
                            account.Email = reader.GetString(4);
                            account.AvatarPath = reader.GetString(5);
                            account.Description = reader.GetString(6);
                        }
                    }
                    conn.Close();

                    return account;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static List<Product> GetProfileProducts(int userId)
        {
            try
            {
                List<Product> products = new List<Product>();

                string connStr = GetConnectionString();

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    MySqlCommand comm1 = conn.CreateCommand();
                    comm1.CommandText = "SELECT Product.Image, Product.Name, Product.Description FROM Product WHERE ProductID IN (SELECT License.ProductID FROM License WHERE UserID = '" + userId + "')";

                    using (MySqlDataReader reader = comm1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product prod = new Product();
                            prod.Image = reader.GetString(0);
                            prod.Name = reader.GetString(1);
                            prod.Description = reader.GetString(2);
                            products.Add(prod);
                        }
                    }

                    conn.Close();

                    return products;
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static List<Comment> GetProfileComments(int userId)
        {
            try
            {
                List<Comment> comments = new List<Comment>();

                string connStr = GetConnectionString();
               

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    MySqlCommand comm1 = conn.CreateCommand();
                    comm1.CommandText = "SELECT CommentTXT, CONCAT(CreatedAT) as CreatedAtS FROM Comment WHERE ProfileID=(SELECT pr.ProfileID FROM Profile as pr WHERE UserID='" + userId + "')";

                    using (MySqlDataReader reader = comm1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Comment cmt = new Comment();
                            cmt.CommentTXT = reader.GetString(0);

                            if (Convert.ToString(reader.GetString(1)) != null)
                            {
                                cmt.CreatedAt = reader.GetString(1);
                            }
                            else
                            {
                                cmt.CreatedAt = "Not available";
                            }

                            comments.Add(cmt);
                        }
                    }

                    conn.Close();

                    return comments;
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static void AddComment(string comment, int userId)
        {

            try
            {

                int profileid = GetProfileID(userId);
                string connStr = GetConnectionString();
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    MySqlCommand comm = conn.CreateCommand();

                    comm.CommandText = @"INSERT INTO Comment(CommentTXT, ProfileID, CreatedAt, fk_CommentID) VALUES(@comment, @profileid, @created, @commentid)";
                    comm.Parameters.AddWithValue("@comment", comment);
                    comm.Parameters.AddWithValue("@profileid", profileid);
                    comm.Parameters.AddWithValue("@created", "GETDATE()");
                    comm.Parameters.AddWithValue("@commentid", null);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Comment successfully added!");
                    

                }


            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;

                    default:
                        MessageBox.Show(ex.Message);
                        break;

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        public static int GetProfileID(int userID)
        {
            string profile = "0";
            int profileint = 0;

            try
            {

                string connStr = GetConnectionString();
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand comm2 = conn.CreateCommand();
                    comm2.CommandText = @"SELECT pr.ProfileID FROM Profile as pr WHERE UserID=@user";
                    comm2.Parameters.AddWithValue("@user", userID);
                    comm2.ExecuteNonQuery();
                    using (MySqlDataReader reader = comm2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            profile = reader.GetString(0);
                            profileint = Convert.ToInt32(profile);
                            return profileint;
                        }
                    }

                }


            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;

                    default:
                        MessageBox.Show(ex.Message);
                        break;

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            return profileint;

        }

        private static string GetConnectionString()
        {

            string password = GamePlanet.Properties.Settings.Default.DBPassword; // DBPassword reference;
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8292_2;user=K8292;password={0};allow zero datetime=false", password);
        }

    }
}
   




