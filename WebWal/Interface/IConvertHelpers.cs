using System.Collections.Generic;
using WebWal.Helpers;

namespace WebWal.Interface
{
    public interface IConvertHelpers
    {
        public string ReturnWebRateData();
        public Dictionary<string, Currency> GetCurrencies();
        public decimal ParseData(Currency Currency);
    }
}
