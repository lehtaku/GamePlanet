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
                SqlConnect connection = new SqlConnect();
                dgProducts.ItemsSource = connection.GetProducts();
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
            Product row = (Product)dgProducts.SelectedItems[0];
            string description = row.Description;
            txbDescription.Text = description;

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
    }

}
