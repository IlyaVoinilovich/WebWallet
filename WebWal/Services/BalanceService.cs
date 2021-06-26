using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWal.Interface;
using WebWal.Models;

namespace WebWal.Services
{
    public class BalanceService: IBalance
    {
        private readonly WalletDbContextcs _context;

        public BalanceService(WalletDbContextcs context)
        {
            _context = context;
        }

        public async Task<ActionResult<BalanceInfo>> Balance(long idUser)
        {
            var _wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == idUser);
            return new BalanceInfo(_wallet);
        }
    }
}
