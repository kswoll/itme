using System;
using ItMe.Models;

namespace ItMe.Database
{
    public class DbBlogPostComment : DbEntity
    {
        public int BlogPostId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public BlogPostCommentStatus Status { get; set; }
        public bool ShouldNotifyOnApproval { get; set; }

        public DbBlogPost BlogPost { get; set; }
    }
}