using System.Collections.Generic;

namespace ItMe.Database
{
    public class DbCv : DbPersonEntity
    {
        public string Blurb { get; set; }

        public List<DbJob> Jobs { get; set; }
        public List<DbExternalProfile> Profiles { get; set; }
        public List<DbLanguage> Languages { get; set; }
        public List<DbSkill> Skills { get; set; }
    }
}