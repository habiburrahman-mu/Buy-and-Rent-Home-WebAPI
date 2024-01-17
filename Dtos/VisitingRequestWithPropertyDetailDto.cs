namespace BuyAndRentHomeWebAPI.Dtos
{
        public class VisitingRequestWithPropertyDetailDto: VisitingRequestDetailDto
        {
            public int VisitingRequestId { get; set; }
            public string Name { get; set; }
            public string TakenByName { get; set; }
        }
}
