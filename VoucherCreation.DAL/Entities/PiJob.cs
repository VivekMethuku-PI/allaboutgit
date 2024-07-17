using System;
using System.Collections.Generic;

namespace VoucherCreation.DAL.Entities;

public partial class PiJob
{
    public long Id { get; set; }

    public int AccountId { get; set; }

    public byte Bgtype { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedDate { get; set; }
}
