namespace BuyandRentHomeWebAPI.Dtos
{
    public class PaginationParameter
    {
        public int CurrentPageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool IsDescending { get; set; }
        public string SearchField { get; set; }
        public string SearchingText { get; set; }
    }
}
