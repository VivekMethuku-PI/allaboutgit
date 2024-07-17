using Microsoft.Extensions.Logging;
using System.Reflection;
using VoucherCreation.BAL.Infrastructure.AppSettingsConfig;
using VoucherCreation.BAL.IRepositories;
using VoucherCreation.BAL.Repositories;
using VoucherCreation.Common.Logging;
using VoucherCreation.DAL;

namespace ws_BAL.Repositories
{
    public class SecurityRepository : BaseRepository, ISecurityRepository
    {
        private Logger logger;
        public SecurityRepository(PiVMSContext dbContext, AppSettings appSettings, ILogger<SecurityRepository> logger) : base(dbContext, appSettings)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            this.logger = new Logger(logger, GetType(), dbContext);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel">LogLevel</param>
        /// <param name="loggingEvent">LoggingEvents</param>
        /// <param name="msg">message</param>
        /// <param name="RequestID"></param>
        public void LogMessage(LogLevel logLevel, int loggingEvent, string msg, string RequestID)
        {
            logger.SetMethodName(MethodBase.GetCurrentMethod());
            logger.Log(logLevel, loggingEvent, msg, RequestID);
        }

    }
}
