using System;
using System.Collections.Generic;

#nullable disable

namespace News_Api.Models
{
    public partial class CategoryNews
    {
        public int CategoryId { get; set; }
        public int NewsId { get; set; }

        public virtual Category Category { get; set; }
        public virtual News News { get; set; }
    }
}
