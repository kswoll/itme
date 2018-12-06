using ItMe.Models;

namespace ItMe.ViewModels
{
    public class BlogViewComponentModel
    {
        public Person Person { get; set; }
        public BlogPost[] BlogPosts { get; set; }
    }
}