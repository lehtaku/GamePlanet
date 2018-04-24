using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanet
{
    class Cart
    {
        public double Totalprice { get; set; }
        public int Cartsum { get; set; }
        public List<Product> cart = new List<Product>();

        public void AddProducts(string image, string name, double price, string description)
        {
            cart.Add(new Product(image, name, price, description));
        }

        public string ShowItems()
        {
            return string.Join("", cart.Select(Product => "Name: " + Product.Name + " Price: " + Product.Price).ToArray());
        }


        public void Emptycart()
        {
            cart.Clear();
        }
    }

}

