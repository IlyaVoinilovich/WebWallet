using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWal.Helpers;
using WebWal.Interface;
using WebWal.Models;

namespace WebWal.Services
{
    public class DepositService:IDeposit
    {
        private readonly WalletDbContextcs _context;

        public DepositService( WalletDbContextcs context)
        {
            _context = context;
        }

        public BalanceInfo Deposit(DepositCommand command, UserWallet wallet)
        {
                wallet.AddBalance(command.Balance);
                _context.Entry(wallet).State = EntityState.Modified;
                _context.SaveChanges();
                return new BalanceInfo(wallet);
        }
        public async Task<BalanceInfo> NewDeposit(DepositCommand command, long UserId)
        {
            var entity = new UserWallet(command.Balance, command.Currency, UserId);
            await _context.Wallets.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new BalanceInfo(entity);
        }
    }
}

