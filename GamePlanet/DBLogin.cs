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
                    comm.CommandText = "INSERT INTO User(FirstName, LastName, Username, Email, Password, CreatedAt) VALUES(@fname, @lname, @uname, @email, @pword, @cat)";
                    comm.Parameters.AddWithValue("@fname", firstname);
                    comm.Parameters.AddWithValue("@lname", lastname);
                    comm.Parameters.AddWithValue("@uname", username);
                    comm.Parameters.AddWithValue("@email", email);
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

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "SELECT UserID, FirstName, LastName, UserName, Email, AvatarPath, Description FROM User WHERE UserName = '" + username + "'";

                    using (MySqlDataReader reader = comm.ExecuteReader())
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

        private static string GetConnectionString()
        {

            string password = GamePlanet.Properties.Settings.Default.DBPassword; // DBPassword reference;
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8292_2;user=K8292;password={0}", password);
        }
    }

}


