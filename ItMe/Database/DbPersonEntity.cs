namespace ItMe.Database
{
    public class DbPersonEntity : DbEntity
    {
        public int PersonId { get; set; }

        public DbPerson Person { get; set; }
    }
}