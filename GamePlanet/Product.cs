using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanet
{
    public class Product
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public Product()
        {
            
        }

        public Product(string name, string image, string description)
        {
            this.Name = name;
            this.Image = image;
            this.Description = description;
        }
    }
}
