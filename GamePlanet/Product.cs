using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanet
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public Product()
        {
            
        }

        public Product(int productId, string name, string image, double price, string description)
        {
            this.ProductID = productId;
            this.Name = name;
            this.Image = image;
            this.Price = price;
            this.Description = description;
        }
    }
}
