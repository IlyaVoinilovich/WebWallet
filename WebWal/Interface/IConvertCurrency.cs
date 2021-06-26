using WebWal.Helpers;

namespace WebWal.Interface
{
    public interface IConvertCurrency
    {
        public decimal ConvertCurrency(decimal balance, decimal fromRate, decimal toRate);
        public decimal ExchangeRate(decimal balance, Currency fromCurrency, Currency toCurrency);
    }
}
