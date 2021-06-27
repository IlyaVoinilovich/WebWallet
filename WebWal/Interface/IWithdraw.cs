

using System.Threading.Tasks;
using WebWal.Models;

namespace WebWal.Interface
{
    public interface IWithdraw
    {
        public Task<BalanceInfo> Withdraw(WithdrawCommand command, UserWallet wallet);
    }
}
