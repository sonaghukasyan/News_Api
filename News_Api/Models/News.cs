using System;
using System.Collections.Generic;

#nullable disable

namespace News_Api.Models
{
    public partial class News
    {
        public News()
        {
            CategoryNews = new HashSet<CategoryNews>();
        }

        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ICollection<CategoryNews> CategoryNews { get; set; }
    }
}
