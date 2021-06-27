using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebWal.Helpers.Enums;
using WebWal.Interface;
using WebWal.Models;

namespace WebWal.Helpers
{
    public class ConvertCurrencyService: IConvertCurrency
    {
        private string ApiKey = "b46d6d763f22a14e01c7";

        public decimal Convert(decimal amount, string from, string to)
        {
            return ConvertCurrencyHelpers.ExchangeRate(from, to, ApiKey) * amount;
        }

        public async Task<decimal> ConvertAsync(decimal amount, string from, string to)
        {
            return await Task.Run(() => Convert(amount, from, to));
        }
        public BalanceInfo ConvertWallet(ConvertCommand command, UserWallet wallet)
        {
            var balance = Convert(wallet.Balance, command.fromCurrency, command.ToCurrency);
            wallet.Balance = balance;
            wallet.Currency = command.ToCurrency;
            return new BalanceInfo(wallet);
        }
    }
}
