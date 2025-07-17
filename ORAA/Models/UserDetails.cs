namespace ORAA.Models
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public List<Purchase> Purchases { get; set; }

    }
}
