using WebWal.Interface;
using WebWal.Models;

namespace WebWal.Services
{
    public class WithdrawService: IWithdraw
    {
            private readonly IConvertCurrency _convertCurrency;

            public WithdrawService(IConvertCurrency convertCurrency)
            {
                _convertCurrency = convertCurrency;
            }

            public  BalanceInfo Withdraw(WithdrawCommand command,UserWallet wallet)
            {
                var balance = _convertCurrency.ExchangeRate(wallet.Balance, wallet.Currency, command.Currency);
                wallet.Currency = command.Currency;
                wallet.Balance = balance;
                wallet.SubtractBalance(command.Withdraw);
                return new BalanceInfo(wallet);
            }
    }
}
