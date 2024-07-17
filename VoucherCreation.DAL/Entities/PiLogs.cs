using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCreation.DAL.Entities
{
    public partial class PiLogs
    {
        public long Id { get; set; }
        public string MethodName { get; set; } = null!;
        public string RequestObject { get; set; } = null!;
        public string ExecptionMessage { get; set; } = null!;
        public int? AccountId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }     
    }
}
