namespace BuyandRentHomeWebAPI.Dtos
{
    public class VisitingRequestWithPropertyDetailDto: VisitingRequestDetailDto
    {
        public int VisitingRequestId { get; set; }
        public string Name { get; set; }
    }
}
