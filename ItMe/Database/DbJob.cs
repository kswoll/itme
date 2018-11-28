using ItMe.Utils;

namespace ItMe.Database
{
    public class DbJob : DbEntity
    {
        public string Company { get; set; }
        public PartialDate Start { get; set; }
        public PartialDate End { get; set; }
    }
}