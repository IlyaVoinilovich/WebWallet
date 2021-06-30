using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWal.Interface;
using WebWal.Models;
using WebWal.ModelsModels;

namespace WebWal.Services
{
    public class WebWalletService: IWebWallet
    {
         private readonly WalletDbContextcs _context;
        public WebWalletService(WalletDbContextcs context)
        {
            _context = context;
        }

        public bool NewDeposit(CreateTransactionCommand command)
        {
            var entity = new UserWallet(command.balance, command.currency, command.idUser);
            _context.Wallets.AddAsync(entity);
            _context.SaveChangesAsync();
            return true;
        }
        public bool Deposit(CreateTransactionCommand command, UserWallet wallet)
        {
            wallet.AddBalance(command.balance);
            _context.Entry(wallet).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return true;
        }
        public bool Withdraw(CreateTransactionCommand command, UserWallet wallet)
        {
            if(wallet.Balance<=command.balance)
            {
                return false;
            }
            wallet.SubtractBalance(command.balance);
            _context.Entry(wallet).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return true;
        }
        public bool TransactionCreate(CreateTransactionCommand command)
        {
            var wallet =  _context.Wallets.FirstOrDefault(x => x.UserId == command.idUser && x.Currency == command.currency);
            if (command.balance >= 0)
            {
                if (wallet == null)
                {
                    return NewDeposit(command);
                }
                return  Deposit(command, wallet);
            }
            else
            {
                if (wallet == null)
                {
                    return false;
                }
                return Withdraw(command, wallet);
            }
        }

    }
}
