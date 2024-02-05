namespace BuyAndRentHomeWebAPI.Specification.Constants
{
    public enum UserRoleIds
    {
        // id from db
        Admin = 5,
        AreaManager = 8,
        User = 10
    }

    //public enum UserRoleNames
    //{
    //    Admin = "Admin",
    //    AreaManager = "Area Manager",
    //    User = 18
    //}

    public enum VisitingRequestStatus
    {
        Pending = 'P',
        Approved = 'A',
        NotApproved = 'N'
    }
}
