using System;
using System.Collections.Generic;
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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        public Cart()
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
            dgCart.ItemsSource = Globals.ShoppingCart;
            txbCart.Text = "Cart: " + Globals.CartPrice.ToString() + " €";
            totalPrice.Text = "Total Price: " + Globals.CartPrice.ToString() + " €";
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
            Shop shop = new Shop();
            shop.Show();
            this.Close();
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            DBProduct.SaveOrder();
            Globals.ShoppingCart.Clear();
            Globals.CartPrice = 0;
            Profile profile = new Profile(Globals.Username);
            profile.Show();
            this.Close();
        }
    }
}
