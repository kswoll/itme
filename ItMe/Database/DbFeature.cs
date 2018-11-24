using ItMe.Models;

namespace ItMe.Database
{
    public class DbFeature : DbEntity
    {
        public int PersonId { get; set; }
        public FeatureType Type { get; set; }

        public DbPerson Person { get; set; }
    }
}