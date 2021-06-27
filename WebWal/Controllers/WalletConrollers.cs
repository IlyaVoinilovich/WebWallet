using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebWal.Interface;
using WebWal.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebWal.Helpers.FakeUserApi.Models;
using System.Collections.Generic;

namespace WebWal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WalletConrollers : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly WalletDbContextcs _context;
        private readonly IWithdraw _withdraw;
        private readonly IDeposit _deposit;
        private readonly IConvertCurrency _convertCurrency;
        private readonly ILogger<WalletConrollers> _logger;

        private long UserId => long.Parse(User.Claims.Single(c => string.IsNullOrEmpty(c.Type)|| c.Type == ClaimTypes.NameIdentifier).Value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public WalletConrollers(WalletDbContextcs context,  IWithdraw withdraw, IDeposit deposit, IConvertCurrency convertCurrency, ILogger<WalletConrollers> logger)
        {
            _context = context;
            _withdraw = withdraw;
            _deposit = deposit;
            _convertCurrency = convertCurrency;
            _logger = logger;
        }
        /// <summary>
        ///     Get the state of your wallet (the amount of money in each currency).
        /// </summary>
        /// <returns>The user's balance.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BalanceInfo>>> Balance()
        {
            return await _context.Wallets
               .AsNoTracking()
               .Where(x => x.UserId == UserId)
               .Select(x => new BalanceInfo(x)).ToArrayAsync();
        }

        /// <summary>
        ///     Top up user wallet in one of the currencies.
        /// </summary>
        /// <param name="command">Request body.</param>
        /// <response code="200">If the wallet has been successfully replenished.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        [Authorize]
        [HttpPost("deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BalanceInfo>> Deposit([FromBody] DepositCommand command)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == UserId && x.Currency == command.Currency);
            if (wallet == null)
            {
                _logger.LogInformation(LogEvents.InsertItem, "New Wallet User {id}", UserId);
                return Ok(await _deposit.NewDeposit(command, UserId));
            }
            _logger.LogInformation(LogEvents.UpdateItem, "Update balance User {id}", UserId);
            return Ok(await _deposit.Deposit(command,wallet));
        }
        /// <summary>
        ///     Withdraw money in one of the currencies.
        /// </summary>
        /// <param name="command">Request body.</param>
        /// <response code="200">If the money was successfully withdrawn.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        /// <response code="404">If the user is not found.</response>
        [Authorize]
        [HttpPost("withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BalanceInfo>> Withdraw([FromBody] WithdrawCommand command)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == UserId && x.Currency == command.Currency);
            if (wallet == null)
            {
                _logger.LogInformation(LogEvents.GetItemNotFound, "Wallet not found");
                return NotFound();
            }

            if (wallet.Balance < command.Withdraw)
            {
                _logger.LogWarning(LogEvents.GenerateItems, "Wallet can't withdraw balance User {id}", UserId);
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok(_withdraw.Withdraw(command, wallet));
        }

        /// <summary>
        ///     Transfer money from one currency to another.
        /// </summary>
        /// <param name="command">Request body.</param>
        /// <response code="200">If the currency was successfully converted to another currency.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        /// <response code="404">If the user is not found.</response>
        [Authorize]
        [HttpPost("convert")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BalanceInfo>> Convert([FromBody] ConvertCommand command)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == UserId && x.Currency==command.fromCurrency);
            if (wallet == null)
            {
                _logger.LogInformation(LogEvents.GetItemNotFound, "Wallet not found");
                return NotFound();
            }
            _logger.LogInformation(LogEvents.UpdateItem, "Wallet currency convert User {id}", UserId);
            await _context.SaveChangesAsync();
            return Ok(_convertCurrency.ConvertWallet(command,wallet));
        }
    }
}

