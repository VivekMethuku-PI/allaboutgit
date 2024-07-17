using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherCreation.DAL.Entities
{
    public partial class PiCountry
    {
        public short Id { get; set; }
        public string Iso { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string NiceName { get; set; } = null!;
        public string Iso3 { get; set; } = null!;
        public string? Numcode { get; set; }
        public string? Phonecode { get; set; }
        public string Currency { get; set; } = null!;
        public string CurSymbol { get; set; } = null!;
    }
}
