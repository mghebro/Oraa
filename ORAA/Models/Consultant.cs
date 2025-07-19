namespace ORAA.Models
{
    public class Consultant
    {
        public int Id { get; set; }
        public string Specialization { get; set; }
        public string Certification { get; set; }
        public string MondayAvailability { get; set; }   // "09:00-17:00"
        public string TuesdayAvailability { get; set; }
        public string WednesdayAvailability { get; set; }
        public string ThursdayAvailability { get; set; }
        public string FridayAvailability { get; set; }
        public string SaturdayAvailability { get; set; }
        public string SundayAvailability { get; set; }
        public string Languages { get; set; }
        public decimal Rating { get; set; }
        public int TotalConsultations { get; set; }
        public string Bio { get; set; }
        public string Qualifications { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public User User { get; set; }
        public int UserId { get; set; }
        public Chat Chat { get; set; }
        public int? ChatId { get; set; }
        public Notification Notification { get; set; }

    }
}
