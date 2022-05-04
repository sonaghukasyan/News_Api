using System;
using System.Collections.Generic;

#nullable disable

namespace News_Api.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryNews = new HashSet<CategoryNews>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CategoryNews> CategoryNews { get; set; }
    }
}
