using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanet
{
    public class Comment
    {

        public string CommentTXT { get; set; }
        public string CreatedAt { get; set; }

        public Comment()
        {

        }

        public Comment(string commentTXT, string createdAt)
        {
            this.CommentTXT = commentTXT;
            this.CreatedAt = createdAt;

        }


    }
}
