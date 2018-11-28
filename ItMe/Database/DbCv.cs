using System.Collections.Generic;

namespace ItMe.Database
{
    public class DbCv : DbPersonEntity
    {
        public List<DbJob> Jobs { get; set; }
    }
}