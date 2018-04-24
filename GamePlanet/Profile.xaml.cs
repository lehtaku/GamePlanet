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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {

        // Constructors
        public Profile()
        {
            
        }

        public Profile(string username)
        {
            InitializeComponent();
            GetProfile(username);
            GlobalizeUser(username);
        }

        // Methods
        private void GlobalizeUser(string username)
        {
            Globals.Username = username;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void GetProfile(string username)
        {
            // Welcome message
            User account = DBLogin.MySQLGetProfile(username);
            Usrname.Text = "Welcome Back " + account.Username + "!";

            // Avatar image
            if (String.IsNullOrEmpty(account.AvatarPath))
            {
                string path = "Media/Avatar/blank-avatar.png";
                PathToImgSource(path);
            }
            else
            {               
                PathToImgSource(account.AvatarPath);
            }

            // Profile's name etc..
            Fullname.Text = account.Firstname + " " + account.Lastname;
            Description.Text = account.Description;

            // Profile's owned products
            myProducts.ItemsSource = DBLogin.GetProfileProducts(account.UserID);

            myComments.ItemsSource = DBLogin.GetProfileComments(account.UserID);

        }

        private void PathToImgSource(string imgPath)
        {
            string avatarPath = imgPath;
            Uri imageUri = new Uri(avatarPath, UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            Image myImage = new Image();
            Avatar.Source = imageBitmap;
        }

        private void btnStore_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = new Shop();
            shop.Show();
            this.Close();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
