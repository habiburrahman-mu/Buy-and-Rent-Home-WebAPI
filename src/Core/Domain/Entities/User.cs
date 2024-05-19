using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public byte[] PasswordKey { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public virtual ICollection<ChatMessage> ChatMessageReceivers { get; set; } = new List<ChatMessage>();

    public virtual ICollection<ChatMessage> ChatMessageSenders { get; set; } = new List<ChatMessage>();

    public virtual ICollection<CitiesAreaManager> CitiesAreaManagerLastUpdatedByNavigations { get; set; } = new List<CitiesAreaManager>();

    public virtual ICollection<CitiesAreaManager> CitiesAreaManagerManagers { get; set; } = new List<CitiesAreaManager>();

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual ICollection<Role> RoleCreatedByNavigations { get; set; } = new List<Role>();

    public virtual ICollection<Role> RoleUpdatedByNavigations { get; set; } = new List<Role>();

    public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();

    public virtual ICollection<VisitingRequest> VisitingRequests { get; set; } = new List<VisitingRequest>();
}
