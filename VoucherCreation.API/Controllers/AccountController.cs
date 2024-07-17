using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoucherCreation.BAL.IRepositories;

namespace VoucherCreation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        #region READONLY VARIABLES
        private readonly IAccountRepository repository;
        #endregion

        #region CONSTRUCTOR
        public AccountController(IAccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        #endregion
    }
}
