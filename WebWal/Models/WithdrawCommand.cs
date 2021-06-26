using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWal.Helpers;

namespace WebWal.Models
{
    public class WithdrawCommand
    {
        /// <summary>
        ///     The amount you need to cash out.
        /// </summary>
        public decimal Withdraw { get; set; }

        /// <summary>
        ///     The currency that the amount will be cashed from.
        /// </summary>
        public Currency Currency { get; set; }
    }
}
