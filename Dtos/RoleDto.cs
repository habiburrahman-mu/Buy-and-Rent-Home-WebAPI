namespace BuyandRentHomeWebAPI.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public string Description { get; set; }
    }
}
