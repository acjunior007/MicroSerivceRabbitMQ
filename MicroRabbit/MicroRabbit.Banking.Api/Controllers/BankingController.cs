using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly ILogger<BankingController> _logger;
        private readonly IAccountService _accountService;

        public BankingController(ILogger<BankingController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        // Get api/banking
        [HttpGet]
        public ActionResult<IEnumerable<IEnumerable<Account>>> Get()
        {
            try
            {
                var accounts = _accountService.GetAccounts();
                return (Ok(accounts));
            }
            catch (Exception ex)
            {
                return (BadRequest(ex.Message));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountTransfer accountTransfer)
        {
            try
            {
                _accountService.Transfer(accountTransfer);
                return Ok(accountTransfer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}