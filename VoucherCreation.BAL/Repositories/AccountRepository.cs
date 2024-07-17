using Microsoft.Extensions.Logging;
using VoucherCreation.BAL.Infrastructure.AppSettingsConfig;
using VoucherCreation.BAL.IRepositories;
using VoucherCreation.Common.Logging;
using VoucherCreation.DAL;


namespace VoucherCreation.BAL.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        readonly Logger logger;
        ILogger<AccountRepository> _logger;
        public AccountRepository(PiVMSContext dbContext, ILogger<AccountRepository> logger,
    AppSettings appSettings) : base(dbContext, logger, appSettings)
        {
            this.logger = new Logger(logger, GetType(), dbContext);
            this._logger = logger;
        }

    }
}
