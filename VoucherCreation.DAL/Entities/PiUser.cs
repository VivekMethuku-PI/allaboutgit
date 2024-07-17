using System;
using System.Collections.Generic;

namespace VoucherCreation.DAL.Entities;

public partial class PiUser
{
    public int UserId { get; set; }

    public int AccountId { get; set; }

    public string Email { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
