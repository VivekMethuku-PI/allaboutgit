using Microsoft.Extensions.Logging;
using Service.ViewModels;
using System.Reflection;
using VoucherCreation.BAL.Infrastructure.AppSettingsConfig;
using VoucherCreation.BAL.IRepositories;
using VoucherCreation.BAL.ViewModels;
using VoucherCreation.Common.Logging;
using VoucherCreation.DAL;
using VoucherCreation.DAL.Entities;

namespace VoucherCreation.BAL.Repositories
{
    public class VoucherRepository : BaseRepository, IVoucherRepository
    {
        readonly Logger logger;
        ILogger<VoucherRepository> _logger;
        public VoucherRepository(PiVMSContext dbContext, ILogger<VoucherRepository> logger,
    AppSettings appSettings) : base(dbContext, logger, appSettings)
        {
            this.logger = new Logger(logger, GetType(), dbContext);
            this._logger = logger;
        }

        public async Task<CreateResponseViewModel<long>> CreateVoucherAsync(CreateVoucherViewModel model)
        {
            logger.SetMethodName(MethodBase.GetCurrentMethod());
            var response = new CreateResponseViewModel<long>();

            int UserId = model.CreatedBy;
            int AccountId = model.AccountId;

            var isLimitAvail = await GetFeatureTypeLimit(model.AccountId);

            try
            {
                if (isLimitAvail)
                {
                    var piVoucher = new PiVoucher
                    {
                        AccountId = AccountId,
                        Amount = model.Amount,
                        Code = model.Code,
                        CreatedBy = UserId,
                        Currency = model.Currency,
                        ExpiryDate = model.ExpiryDate == null ? currentTime.AddDays(30) : model.ExpiryDate.Value
                    };

                    dbContext.PiVouchers.Add(piVoucher);
                    await dbContext.SaveChangesAsync();

                    response.SetResult(piVoucher.VoucherId);
                }
                else
                {
                    response.SetError(Service.BAL.AppConstants.ApiResponseErrorCodes.LimtExhausted,"Limit is expired");
                }

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, LoggingEvents.InsertItem, "/" + " CreateVoucher Request:" + Newtonsoft.Json.JsonConvert.SerializeObject(model), e);
            }

            return response;
        }

    }
}
