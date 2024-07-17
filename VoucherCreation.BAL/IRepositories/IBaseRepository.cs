
using VoucherCreation.Common.Logging;

namespace VoucherCreation.BAL.IRepositories
{
    public interface IBaseRepository
    {       
        Logger logger { get; set; }
    }
  

}
