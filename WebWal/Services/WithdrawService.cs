using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebWal.Interface;
using WebWal.Models;

namespace WebWal.Services
{
    public class WithdrawService: IWithdraw
    {
        private readonly WalletDbContextcs _context;

        public WithdrawService(WalletDbContextcs context)
        {
            _context= context; 
        }

        public async Task<BalanceInfo> Withdraw(WithdrawCommand command,UserWallet wallet)
        {
            wallet.SubtractBalance(command.Withdraw);
            _context.Entry(wallet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new BalanceInfo(wallet);
        }
    }
}
