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
            Username = username;
            Password = password;
        }

        public Login()
        {
        }




    }

}
