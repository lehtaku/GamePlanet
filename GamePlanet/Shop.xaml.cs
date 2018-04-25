using Microsoft.Win32;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GamePlanet
{
    /// <summary>
    /// Interaction logic for Shop.xaml
    /// </summary>
    public partial class Shop : Window
    {
        public Shop()
        {
            InitializeComponent();
            
            FillData();
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void FillData()
        {
            try
            {
                dgProducts.ItemsSource = DBProduct.GetProducts();
                txbCart.Text = "Cart: " + Globals.CartPrice.ToString() + " €";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On click show product information
            stpProductinfo.Visibility = System.Windows.Visibility.Visible;

            // Bind product information

            object item = dgProducts.SelectedItem;
            string title = (dgProducts.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            prodTitle.Text = title;
            

            // Show description

            Product row = (Product)dgProducts.SelectedItems[0];
            string description = row.Description;
            txbDescription.Text = description;

            // Bind ProductID
            int prodID = row.ProductID;
            
            // Update purchase button

            double prdPrice = row.Price;
            string priceDisplay = ("Add to cart " + prdPrice + "€").ToString();
            btnPurchase.Content = priceDisplay;

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            string username = Globals.Username;
            Profile profile = new Profile(username);
            profile.Show();
            this.Close();
        }

        private void btnStore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            object item = dgProducts.SelectedItem;

            // Description 

            Product row = (Product)dgProducts.SelectedItems[0];
            string prdDescription = row.Description;

            // Price

            double prdPrice = row.Price;

            // Image

            string prdImg = row.Image;

            // Name

            string prdName = row.Name;

            // ProductID
            int prdID = row.ProductID;

            // Add items to cart
            Globals.ShoppingCart.Add(new Product(row.ProductID, row.Name, row.Image, row.Price, row.Description));
            Globals.CartPrice += row.Price;
            txbCart.Text = "Cart: " + Globals.CartPrice.ToString() + " €";

        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            Cart cart = new Cart();
            cart.Show();
            this.Close();
        }

        private void btnLogin3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}


