using System;
using System.ComponentModel.DataAnnotations;

namespace ORAA.Request
{
    
    public class AddProductDetailsRequest
    {
        
        public string Title { get; set; }

    
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
