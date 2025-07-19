namespace ORAA.DTO
{
    public class OrderHistoryDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
