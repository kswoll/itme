namespace ItMe.Database
{
    public class DbExternalProfile : DbEntity
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public int CvId { get; set; }

        public DbCv Cv { get; set; }
    }
}