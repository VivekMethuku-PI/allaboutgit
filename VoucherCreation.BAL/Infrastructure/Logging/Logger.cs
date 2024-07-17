using Microsoft.Extensions.Logging;
using System.Reflection;
using VoucherCreation.DAL;

namespace VoucherCreation.Common.Logging
{
    public class Logger
    {
        ILogger logger;
        internal PiVMSContext dbContext;
        string ClsNm = string.Empty, MethodName = string.Empty;
        public Logger(ILogger logger, Type ClassType, PiVMSContext dbContext)
        {
            this.logger = logger;
            this.ClsNm = ClassType.FullName;
            this.dbContext = dbContext;
        }
        public void SetMethodName(MethodBase methodBase)
        {
            MethodName = methodBase.DeclaringType.Name;
        }

        internal void Log(LogLevel level, int eventId, string msg, Service.BAL.ViewModels.ResponseMetaViewModel meta)
        {
            Log(level, eventId, msg, meta.RequestID, null);
        }
        public void Log(LogLevel level, int eventId, string msg, Service.BAL.ViewModels.ResponseMetaViewModel meta, Exception exception)
        {
            Log(level, eventId, msg, meta.RequestID, exception);
        }
        public void Log(LogLevel level, int eventId, string msg)
        {
            Log(level, eventId, msg, string.Empty, null);
        }

        public void Log(LogLevel level, int eventId, string msg, string RefId)
        {
            Log(level, eventId, msg, RefId, null);
        }
        public void Log(LogLevel level, int eventId, string msg, Exception exception)
        {
            Log(level, eventId, msg, string.Empty, exception);
        }


        public bool isWindowsService = false;
        public void Log(LogLevel level, int eventId, string msg, string RefId, Exception exception)
        {
            try
            {
                var _msg = ClsNm + "/" + MethodName + ":" + msg + (string.IsNullOrEmpty(RefId) ? "" : ", ReferenceId:" + RefId);

                if (exception == null)
                    logger.Log(level, eventId, _msg);
                else
                    logger.Log(level, eventId, exception, _msg);
                if (isWindowsService)
                {
                    var filePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pmVMS Create Service");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    filePath = System.IO.Path.Combine(filePath, DateTime.UtcNow.ToString("yyyy-MM-dd") + (exception == null ? "" : "_exception") + ".txt");
                    System.IO.File.AppendAllLines(filePath, new string[] { DateTime.UtcNow + ":" + _msg + (exception == null ? "" : ", exception:" + (exception.Message + ".InnerException:" + exception?.InnerException?.Message)) });
                }               

            }
            catch (Exception e)
            {
                var filePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pmVMS Create Service");
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                filePath = System.IO.Path.Combine(filePath, DateTime.UtcNow.ToString("log exception") + ".txt");
                System.IO.File.AppendAllLines(filePath, new string[] { DateTime.UtcNow + ":" + (e == null ? "" : ", exception:" + (e.Message + ".InnerException:" + e?.InnerException?.Message)) });
            }
        }
       
    }
}
