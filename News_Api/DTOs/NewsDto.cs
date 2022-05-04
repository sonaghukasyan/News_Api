using System;

namespace News_Api.DTOs
{
    public class NewsDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
