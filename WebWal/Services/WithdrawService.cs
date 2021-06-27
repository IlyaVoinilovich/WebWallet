using Microsoft.EntityFrameworkCore;
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

        public  BalanceInfo Withdraw(WithdrawCommand command,UserWallet wallet)
        {
            wallet.SubtractBalance(command.Withdraw);
            _context.Entry(wallet).State = EntityState.Modified;
            _context.SaveChanges();
            return new BalanceInfo(wallet);
        }
    }
}
