using ItMe.Utils;

namespace ItMe.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public PartialDate Start { get; set; }
        public PartialDate End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}