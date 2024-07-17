using System;
using System.Collections.Generic;

namespace VoucherCreation.DAL.Entities;

public partial class PiVoucherRedemption
{
    public long RedemId { get; set; }

    public int AccountId { get; set; }

    public long VoucherId { get; set; }

    public decimal Amount { get; set; }

    public int Currency { get; set; }

    public short Status { get; set; }

    public DateTime CreatedDate { get; set; }
}
