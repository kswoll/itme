using ItMe.Utils;

namespace ItMe.Database
{
    public class DbJob : DbEntity
    {
        public string Company { get; set; }
        public PartialDate Start { get; set; }
        public PartialDate End { get; set; }
        public int CvId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DbCv Cv { get; set; }
    }
}