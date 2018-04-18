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
        public static void MySQLLogin(string username, string password)
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
                        MessageBox.Show("Login Success");
                    else
                        MessageBox.Show("Incorrect login");
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw;
            }


        }

        private static string GetConnectionString()
        {

            string password = "FjVZyBmmPjCP6TEaYt3gt3K3eYqB3db8";
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8292_2;user=K8292;password={0}", password);
        }
    }
}

