
using WebWal.Helpers;
using WebWal.Interface;

namespace WebWal.Services
{
    public class ConvertService: IConvertCurrency
    {
        private readonly IConvertHelpers _convertHelpers;
        public ConvertService(IConvertHelpers convertHelpers)
        {
            _convertHelpers = convertHelpers;
        }

        // Creates a Dictionary of all Supported Currencies
        public decimal ConvertCurrency(decimal balance,decimal fromRate, decimal toRate)
        {
            var convertedCurrency = (balance * toRate) / fromRate;
            return convertedCurrency;
        }


        // Returns Exchage Rate
        public decimal ExchangeRate(decimal balance,Currency fromCurrency, Currency toCurrency)
        {
            // Gets up-to-date rates
            var fromRate = _convertHelpers.ParseData(fromCurrency);
            var toRate = _convertHelpers.ParseData(toCurrency);
            if (fromRate == 0 || toRate == 0) return 0;
            //Calculation 
            var exchange = ConvertCurrency(balance,fromRate, toRate);
            return exchange;
        }
    }
}
