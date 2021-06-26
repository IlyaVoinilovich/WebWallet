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

namespace WebWal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletConrollers : ControllerBase
    {
        private readonly WalletDbContextcs _context;
        private readonly IBalance _balance;
        private readonly IWithdraw _withdraw;
        private readonly IDeposit _deposit;
        private readonly IConvertCurrency _convertCurrency;
        private readonly ILogger<WalletConrollers> _logger;

        private long UserId => long.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public WalletConrollers(WalletDbContextcs context, IBalance balance, IWithdraw withdraw, IDeposit deposit, IConvertCurrency convertCurrency, ILogger<WalletConrollers> logger)
        {
            _context = context;
            _balance = balance;
            _withdraw = withdraw;
            _deposit = deposit;
            _convertCurrency = convertCurrency;
            _logger = logger;
        }
        /// <summary>
        ///     Get the state of your wallet (the amount of money in each currency).
        /// </summary>
        /// <param name="userId">The primary user key.</param>
        /// <returns>The user's balance.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BalanceInfo>> Balance()
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (wallet == null)
            {
                return NotFound();
            }
                return Ok(_balance.Balance(UserId));
        }

        /// <summary>
        ///     Top up user wallet in one of the currencies.
        /// </summary>
        /// <param name="command">Request body.</param>
        /// <response code="200">If the wallet has been successfully replenished.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BalanceInfo>> Deposit([FromBody] DepositCommand command)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (wallet == null)
            {
                return Ok(_deposit.NewDeposit(command, UserId));
            }
            return Ok(_deposit.Deposit(command,wallet));
        }
        /// <summary>
        ///     Withdraw money in one of the currencies.
        /// </summary>
        /// <param name="command">Request body.</param>
        /// <param name="cancellationToken">Client closed request.</param>
        /// <response code="200">If the money was successfully withdrawn.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        /// <response code="404">If the user is not found.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BalanceInfo>> Withdraw([FromBody] WithdrawCommand command)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (wallet == null)
            {
                _logger.LogInformation(LogEvents.GetItemNotFound, "Wallet not found");
                return NotFound();
            }

            if (wallet.Balance < command.Withdraw)
            {
                _logger.LogWarning(LogEvents.GenerateItems, "Wallet can't withdraw balance");
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok(_withdraw.Withdraw(command, wallet));
        }

        /// <summary>
        ///     Transfer money from one currency to another.
        /// </summary>
        /// <param name="command">Request body.</param>
        /// <param name="cancellationToken">Client closed request.</param>
        /// <response code="200">If the currency was successfully converted to another currency.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        /// <response code="404">If the user is not found.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BalanceInfo>> Convert([FromBody] ConvertCommand command)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (wallet == null)
            {
                _logger.LogInformation(LogEvents.GetItemNotFound, "Wallet not found");
                return NotFound();
            }
            _logger.LogInformation(LogEvents.UpdateItem, "Wallet currency convert");
            return Ok(_convertCurrency.ExchangeRate(wallet.Balance,command.FromCurrency,command.ToCurrency));
        }
    }
}

