using System;
using System.Collections.Generic;

namespace EcommerceTrail.Models;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public int? UsersId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? AddedAt { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? Users { get; set; }
}
