using Service.ViewModels;
using VoucherCreation.BAL.ViewModels;


namespace VoucherCreation.BAL.IRepositories
{
    public interface IVoucherRepository : IBaseRepository
    {
        Task<CreateResponseViewModel<long>> CreateVoucherAsync(CreateVoucherViewModel model);

    }
}
