using System;

namespace ItMe.Models
{
    public class BlogPostComment
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
    }
}