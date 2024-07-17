using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoucherCreation.BAL.IRepositories;
using VoucherCreation.BAL.ViewModels;

namespace VoucherCreation.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class VoucherController : BaseController
    {
        #region READONLY VARIABLES
        private readonly IVoucherRepository repository;
        #endregion

        #region CONSTRUCTOR
        public VoucherController(IVoucherRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        #endregion



        /// <summary>
        /// We can add account specific demographics
        /// by using this service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> CreateVoucherAsync(CreateVoucherViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }              
            
                var demo = await repository.CreateVoucherAsync(model);
                return Ok(demo);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
