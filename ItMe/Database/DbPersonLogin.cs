namespace ItMe.Database
{
    public class DbPersonLogin : DbEntity
    {
		public int PersonId { get; set; }
		public string PasswordHash { get; set; }

		public DbPerson Person { get; set; }
    }
}