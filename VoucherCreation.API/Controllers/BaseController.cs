using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.BAL.ViewModels;
using VoucherCreation.BAL.IRepositories;
using VoucherCreation.Common.Logging;
using static Service.BAL.AppConstants;

namespace VoucherCreation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        /// <summary>
        /// response cache duration
        /// </summary>
        internal const int responseCacheDuration = 300;
        internal const int responseCacheMaxDuration = int.MaxValue;

        IBaseRepository repository;
        public BaseController(IBaseRepository repository)
        {
            this.repository = repository;
        }
        public BaseController(IBaseRepository[] repositories)
        {
            foreach (var repository in repositories) { }
        }


        ISecurityRepository securityRepository;
        [ApiExplorerSettings(IgnoreApi = true)]
        public BadRequestObjectResult BadRequestCustom(ModelStateDictionary modelState)
        {

            var resp = new GetResponseViewModel<string>();
            string msg = string.Empty;
            foreach (var item in ModelState)
            {
                if (item.Value?.Errors != null)
                {
                    foreach (var err in item.Value.Errors)
                    {
                        msg += "," + err.ErrorMessage;
                    }
                }
            }
            resp.SetError(ApiResponseErrorCodes.InvalidJsonFormat, msg.Length > 0 ? msg.Substring(1) : msg, true);
            if (repository?.logger != null)
            {
                repository.logger.Log(LogLevel.Debug, LoggingEvents.MandatoryDataMissing, msg.Length > 0 ? msg.Substring(1) : msg ?? "", resp.Meta.RequestID);
            }
            else
            {
                securityRepository = (ISecurityRepository)Request.HttpContext.RequestServices.GetService(typeof(ISecurityRepository));
                if (securityRepository != null)
                    securityRepository.LogMessage(LogLevel.Debug, LoggingEvents.MandatoryDataMissing, msg.Length > 0 ? msg.Substring(1) : msg ?? "", resp.Meta.RequestID);
            }
            return BadRequest(resp);
        }

    }
}
