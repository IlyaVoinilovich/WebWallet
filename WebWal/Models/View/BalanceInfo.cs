

using WebWal.Helpers;

namespace WebWal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BalanceInfo
    {
        private readonly UserWallet _userWallet;


        public BalanceInfo(UserWallet userWallet)
        {
            _userWallet = userWallet;
        }
        public decimal Balance => _userWallet.Balance;
        public string Currency => _userWallet.Currency;

        }
    }
