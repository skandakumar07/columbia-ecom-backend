using System;
using System.Collections.Generic;

namespace EcommerceTrail.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UsersId { get; set; }

    public int? AddressId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public virtual AddressBook? Address { get; set; }

    public virtual User? Users { get; set; }
}
