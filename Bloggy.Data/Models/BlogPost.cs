using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggy.Data.Interfaces;

namespace Bloggy.Data.Models
{
    public class BlogPost : IBloggyItem
    {
        #region Properties
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        #endregion Properties
    }
}

