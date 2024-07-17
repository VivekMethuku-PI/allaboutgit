using System;
using System.Collections.Generic;

namespace VoucherCreation.DAL.Entities;

public partial class PiRedemptionHistory
{
    public long RedemId { get; set; }

    public int AccountId { get; set; }

    public long VoucherId { get; set; }

    public decimal Amount { get; set; }

    public int Currency { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedDate { get; set; }
}
