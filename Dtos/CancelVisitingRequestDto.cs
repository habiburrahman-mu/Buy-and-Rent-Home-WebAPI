using System.ComponentModel.DataAnnotations;

namespace BuyAndRentHomeWebAPI
{
    public class CancelVisitingRequestDto
    {
        public int VisitingRequestId { get; set; }
        [MaxLength(255)]
        public string CancelReason { get; set; } = null!;
    }
}