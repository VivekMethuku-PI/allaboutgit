using System;
using System.Collections.Generic;

namespace VoucherCreation.DAL.Entities;

public partial class PiVoucherLimitDetail
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int Limit { get; set; }

    public DateTime CreatedDate { get; set; }
}
