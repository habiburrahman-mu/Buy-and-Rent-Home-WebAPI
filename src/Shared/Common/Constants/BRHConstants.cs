namespace Common.Constants;

public enum UserRoleIds
{
    // id from db
    Admin = 5,
    AreaManager = 8,
    User = 10
}

public enum VisitingRequestStatus
{
    Pending = 'P',
    Approved = 'A',
    NotApproved = 'N'
}

public enum PropertyStatus
{
    Active = 'A',
    Draft = 'D',
    Complete = 'C'
}