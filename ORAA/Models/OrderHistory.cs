using ORAA.Enums;

namespace ORAA.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public ORDER_STATUS Status { get; set; }

    }
}
