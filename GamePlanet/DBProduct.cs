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
                    comm1.CommandText = "SELECT ProductID, Name, Image, Description, Price FROM Product";

                    using (MySqlDataReader reader = comm1.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Product prod = new Product();
                            prod.ProductID = reader.GetInt32(0);
                            prod.Name = reader.GetString(1);
                            prod.Image = reader.GetString(2);
                            prod.Description = reader.GetString(3);
                            prod.Price = reader.GetDouble(4);
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

        public static void SaveOrder()
        {
            try
            {
                string connStr = GetConnectionString();

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    for (int i = 0; i < Globals.ShoppingCart.Count; i++)
                    {
                        int prodId = Globals.ShoppingCart[i].ProductID;

                        MySqlCommand comm = conn.CreateCommand();
                        comm.CommandText = "INSERT INTO License (LicenseKey, ProductID, UserID) VALUES('K244-S938', '" + prodId + "', '" + Globals.UserID + "')";
                        comm.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
               
            }        

        }

        private static string GetConnectionString()
        {

            string password = GamePlanet.Properties.Settings.Default.DBPassword; // DBPassword reference;
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial Catalog=K8292_2;user=K8292;password={0}", password);
        }
    }
}
