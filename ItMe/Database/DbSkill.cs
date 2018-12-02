namespace ItMe.Database
{
    public class DbSkill : DbEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CvId { get; set; }

        public DbCv Cv { get; set; }
    }
}