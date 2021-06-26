

using WebWal.Helpers;

namespace WebWal.Models
{
    public class ConvertCommand
    {
        /// <summary>
        ///     The currency that will be converted to another currency.
        /// </summary>
        public Currency FromCurrency { get; set; }

        /// <summary>
        ///     The currency that will be converted to another currency.
        /// </summary>
        public Currency ToCurrency { get; set; }
    }
}
