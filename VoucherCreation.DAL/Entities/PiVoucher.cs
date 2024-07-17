using System;
using System.Collections.Generic;

namespace VoucherCreation.DAL.Entities;

public partial class PiVoucher
{
    public long VoucherId { get; set; }

    public int AccountId { get; set; }

    public string Code { get; set; } = null!;

    public decimal Amount { get; set; }

    public int Currency { get; set; }

    public byte Status { get; set; }
    public DateTime ExpiryDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}


