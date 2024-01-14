using System;
using System.Collections.Generic;

namespace BuyAndRentHomeWebAPI.Data.Entities;

public partial class ChatMessage
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Message { get; set; }

    public DateTime Timestamp { get; set; }

    public virtual User Receiver { get; set; }

    public virtual User Sender { get; set; }
}
