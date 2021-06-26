

namespace WebWal.Helpers
{
    public class Currency
    {
        public string CurrencyCode { get; set; }
        public int CurrencyNum { get; set; }
        public string CurrencyName { get; set; }

        // Overloaded Constructor
        public Currency(string currencyCode, int currencyNum, string currencyName)
        {
            this.CurrencyCode = currencyCode;
            this.CurrencyNum = currencyNum;
            this.CurrencyName = currencyName;
        }
    }
}
