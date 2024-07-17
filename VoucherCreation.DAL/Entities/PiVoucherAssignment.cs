using System;
using System.Collections.Generic;

namespace VoucherCreation.DAL.Entities;

public partial class PiVoucherAssignment
{
    public long VoucherId { get; set; }

    public int AccountId { get; set; }

    public string? Email { get; set; }
    public string? IsdCode { get; set; }

    public int? PhoneNumber { get; set; }
    public int Pin { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }
}
