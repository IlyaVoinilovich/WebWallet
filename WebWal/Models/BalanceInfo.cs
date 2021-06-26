

using WebWal.Helpers;

namespace WebWal.Models
{
    public class BalanceInfo
    {
        private readonly UserWallet _userWallet;


        public BalanceInfo(UserWallet userWallet)
        {
            _userWallet = userWallet;
        }
        public decimal Balance => _userWallet.Balance;
        public Currency Currency => _userWallet.Currency;

        }
    }
