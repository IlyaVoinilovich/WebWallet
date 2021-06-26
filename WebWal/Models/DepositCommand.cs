
using WebWal.Helpers;

namespace WebWal.Models
{
    public class DepositCommand
    {
        /// <summary>
        ///     Amount to be deposited.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        ///     The currency that the amount will be deposited in.
        /// </summary>
        public Currency Currency { get; set; }
    }
}
