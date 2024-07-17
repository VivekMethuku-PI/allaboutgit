
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoucherCreation.BAL.Infrastructure.AppSettingsConfig;
using VoucherCreation.BAL.IRepositories;
using VoucherCreation.Common.Logging;
using VoucherCreation.DAL;

namespace VoucherCreation.BAL.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public Logger logger { get; set; }

        internal PiVMSContext dbContext;
        internal AppSettings appSettings;
        public static DateTime currentTime { get { return DateTime.UtcNow; } }

        public BaseRepository(PiVMSContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public BaseRepository(PiVMSContext dbContext, AppSettings appSettings)
        {
            this.dbContext = dbContext;
            this.appSettings = appSettings;
        }
        public BaseRepository(PiVMSContext dbContext, ILogger logger, AppSettings appSettings)
        {
            this.dbContext = dbContext;
            this.appSettings = appSettings;
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            this.logger = new Logger(logger, GetType(), dbContext);
        }



        #region Database
        public static PiVMSContext getDatabase(string connectionStringSettings)
        {
            return new PiVMSContext(connectionStringSettings);
        }
        #endregion


        internal async Task<bool> GetFeatureTypeLimit(int AccountId)
        {
            bool responseValue = false;
            var repose = await dbContext.PiVoucherLimits.Where(x => x.AccountId == AccountId).FirstOrDefaultAsync();
            if (repose != null)
            {
                if (repose.AvailableLimit > 0)
                {
                    responseValue = true;
                }
            }
            return responseValue;
        }


    }

}
