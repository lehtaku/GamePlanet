using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanet
{
    public class User
    {
        public int UserID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string AvatarPath { get; set; }
        public List<Product> Products { get; set; }
        

        public User()
        {
            Products = new List<Product>();
        }

    }


    
}
