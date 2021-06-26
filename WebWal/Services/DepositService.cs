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
        private readonly IConvertCurrency _convertCurrency;

        public DepositService(IConvertCurrency convertCurrency)
        {
            _convertCurrency = convertCurrency;
        }

        public BalanceInfo Deposit(DepositCommand command, UserWallet wallet)
        {
            if (command.Currency == wallet.Currency)
                wallet.AddBalance(command.Balance);
            else
            {
                return ConvertDeposit(command, wallet);
            }
            return new BalanceInfo(wallet);
        }
        public BalanceInfo NewDeposit(DepositCommand command,long UserId)
        {
            var entity = new UserWallet(command.Balance, command.Currency, UserId);
            return new BalanceInfo(entity);
        }
        public BalanceInfo ConvertDeposit(DepositCommand command, UserWallet wallet)
        {
            var balance = _convertCurrency.ExchangeRate(wallet.Balance, wallet.Currency, command.Currency);
            wallet.Currency = command.Currency;
            wallet.Balance = balance;
            wallet.AddBalance(command.Balance);
            return new BalanceInfo(wallet);
        }
    }
}

