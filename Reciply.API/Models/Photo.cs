namespace Reciply.API.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }

        public string Url { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}