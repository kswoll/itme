using System.Collections.Generic;
using ItMe.Utils;

namespace ItMe.Database
{
    public class DbJob : DbEntity
    {
        public string Company { get; set; }
        public int CvId { get; set; }

        public DbCv Cv { get; set; }
        public List<DbJobRole> Roles { get; set; }
    }
}