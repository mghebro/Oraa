namespace ORAA.DTO
{
    public class CreateOrderDto
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
