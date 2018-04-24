using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GamePlanet
{
    class DBProduct
    {
        public static List<Product> GetProducts()
        {
            try
            {
                List<Product> products = new List<Product>();

                string connStr = GetConnectionString();

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    MySqlCommand comm1 = conn.CreateCommand();
                    comm1.CommandText = "SELECT Image, Name, Description, Price FROM Product";

                    using (MySqlDataReader reader = comm1.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Product prod = new Product();
                            prod.Image = reader.GetString(0);
                            prod.Name = reader.GetString(1);
                            prod.Price = reader.GetDouble(3);
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
    

        private static string GetConnectionString()
        {

            string password = GamePlanet.Properties.Settings.Default.DBPassword; // DBPassword reference;
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8292_2;user=K8292;password={0}", password);
        }
    }
}
