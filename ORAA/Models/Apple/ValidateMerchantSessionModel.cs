using System.ComponentModel.DataAnnotations;

namespace ORAA.Models.Apple
{
    public class ValidateMerchantSessionModel
    {
        [DataType(DataType.Url)]
        [Required]
        public string? ValidationUrl { get; set; }
    }
}
