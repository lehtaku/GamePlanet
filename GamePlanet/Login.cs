using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GamePlanet
{
    class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Login(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public Login()
        {
        }

        public void MySQLLogin(string username, string password)
        {

            try
            {
                string user = username;
                string pw = password;

                string connStr = GetConnectionString();
                string sql = string.Format("SELECT Count(*) FROM Logins WHERE Username = {0} and Password = {1}", user, pw);
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                }
            }
            catch
            {
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
