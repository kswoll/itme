namespace ItMe.Database
{
    public class DbLanguage : DbEntity
    {
        public string Name { get; set; }
        public int CvId { get; set; }

        public DbCv Cv { get; set; }
    }
}