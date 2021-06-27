using System.Threading.Tasks;
using WebWal.Helpers;
using WebWal.Helpers.Enums;
using WebWal.Models;

namespace WebWal.Interface
{
    public interface IConvertCurrency
    {
        public decimal Convert(decimal amount, string from, string to);
        public  Task<decimal> ConvertAsync(decimal amount, string from, string to);
        public BalanceInfo ConvertWallet(ConvertCommand command, UserWallet wallet);
    }
}
