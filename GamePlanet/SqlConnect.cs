using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamePlanet
{
    class SqlConnect
    {
        public MySqlConnection connection { get; set; }
        public string server { get; set; }
        public string database { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        // Constructors
        public SqlConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "mysql.labranet.jamk.fi";
            database = "K8292_2";
            username = "K8292";
            password = "FjVZyBmmPjCP6TEaYt3gt3K3eYqB3db8";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        // Open connection
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
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
                return false;
            }
        }

        // Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        // Get products
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            string query = "SELECT Image, Name, Description, Price FROM Product";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product prod = new Product();
                    prod.Image = reader.GetString(0);
                    prod.Name = reader.GetString(1);
                    prod.Price = reader.GetDouble(3);
                    products.Add(prod);
                }
                return products;
            }
            else
            {
                return products;
            }

        }

    }
}
