

using WebWal.Helpers;

namespace WebWal.Models
{
    public class ConvertCommand
    {
        public string fromCurrency { get; set; }
        /// <summary>
        ///     The currency that will be converted to another currency.
        /// </summary>
        public string ToCurrency { get; set; }
    }
}
