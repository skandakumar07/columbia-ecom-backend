using System;
using System.Collections.Generic;

namespace EcommerceTrail.Models;

public partial class AddressBook
{
    public int AddressId { get; set; }

    public int? UsersId { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? AddressLine { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public string? Country { get; set; }

    public virtual User? Users { get; set; }
}
