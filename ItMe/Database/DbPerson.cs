using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItMe.Database
{
    public class DbPerson : DbEntity
    {
		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }        

        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        public string FavIconS3Key { get; set; }

        public List<DbFeature> Features { get; set; }
    }
}