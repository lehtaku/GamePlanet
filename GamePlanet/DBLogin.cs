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

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result > 0)
                    {
                        MessageBox.Show("Login Success");
                        return result;
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is incorrect");
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

        public static int MySQLCreate(string username, string password)
        {

            try
            {

                string connStr = GetConnectionString();
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO User (FirstName, LastName, UserName, Email, Password, CreatedAt) VALUES", conn);
                    cmd.Parameters.AddWithValue("@uname", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result > 0)
                    {
                        MessageBox.Show("Login Success");
                        return result;
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is incorrect");
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

        private static string GetConnectionString()
        {

            string password = GamePlanet.Properties.Settings.Default.DBPassword; // DBPassword reference;
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8292_2;user=K8292;password={0}", password);
        }
    }

}


