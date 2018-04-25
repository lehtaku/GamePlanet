using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanet
{
    public static class Globals
    {
        public static string Username { get; set; }
        public static int UserID { get; set; }
        public static double CartPrice { get; set; }
        public static List<Product> ShoppingCart = new List<Product>();
    }
}
