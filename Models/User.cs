using System;
using System.Collections.Generic;

namespace EcommerceTrail.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string Typeofuser { get; set; } = null!;

    public virtual ICollection<AddressBook> AddressBooks { get; set; } = new List<AddressBook>();
}
