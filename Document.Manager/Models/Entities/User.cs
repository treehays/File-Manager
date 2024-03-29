﻿using Microsoft.EntityFrameworkCore;

namespace Document.Manager.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    //public string Address { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();

}
