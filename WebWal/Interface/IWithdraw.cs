

using WebWal.Models;

namespace WebWal.Interface
{
    public interface IWithdraw
    {
        public BalanceInfo Withdraw(WithdrawCommand command, UserWallet wallet);
    }
}
