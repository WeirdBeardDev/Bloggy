using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggy.Data.Interfaces;

namespace Bloggy.Data.Models
{
    public class Category : IBloggyItem
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPost> Posts { get; set; }
        #endregion Properties
    }
}
