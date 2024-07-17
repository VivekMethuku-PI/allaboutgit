using System;
using System.Collections.Generic;

namespace VoucherCreation.DAL.Entities;

public partial class PiVoucherLimit
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int TotalLimit { get; set; }

    public int AvailableLimit { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
