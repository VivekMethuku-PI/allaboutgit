using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCreation.BAL.ViewModels
{
    public class CreateVoucherViewModel
    {
        public int AccountId { get; set; }

        public string Code { get; set; } = null!;

        public decimal Amount { get; set; }

        public int Currency { get; set; }      

        public int CreatedBy { get; set; }
        public DateTime? ExpiryDate { get; set; }

    }
}
