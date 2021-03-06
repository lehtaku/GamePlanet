﻿using System;
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
    /// Interaction logic for Createuser.xaml
    /// </summary>
    public partial class Createuser : Window
    {
        public Createuser()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // FirstName, LastName, UserName, Email, Password
            string email = txbEmail.Text;
            string uname = txbUsername.Text;
            string fname = txbFirstname.Text;
            string lname = txbLastname.Text;
            string pword = pwdPassword.Password;

            DBLogin.MySQLCreate(fname, lname, uname, email, pword);


            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();

        }
    }
}

