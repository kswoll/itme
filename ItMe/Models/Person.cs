namespace ItMe.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FavIconS3Key { get; set; }

        public string FavIconS3Url => FavIconS3Key == null ? null : $"https://s3-us-west-2.amazonaws.com/laddler/{FavIconS3Key}";

    }
}