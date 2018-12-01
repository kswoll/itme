using ItMe.Utils;

namespace ItMe.Database
{
    public class DbJobRole : DbEntity
    {
        public PartialDate Start { get; set; }
        public PartialDate End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int JobId { get; set; }

        public DbJob Job { get; set; }
    }
}