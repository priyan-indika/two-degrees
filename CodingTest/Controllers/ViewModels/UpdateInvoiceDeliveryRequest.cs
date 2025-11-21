using CodingTest.Enums;
using System.ComponentModel.DataAnnotations;

namespace CodingTest.Controllers.ViewModels
{
    public class UpdateInvoiceDeliveryRequest
    {
        [Required(ErrorMessage = "DeliveryMethod is required.")]
        [EnumDataType(typeof(InvoiceDeliveryMethod), ErrorMessage = "Invalid delivery method.")]
        public int DeliveryMethod { get; set; }
    }
}
