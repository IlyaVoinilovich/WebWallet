using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebWal.Models;

namespace WebWal.Interface
{
    public interface IBalance
    {
        public Task<ActionResult<BalanceInfo>> Balance (long UserId);
    }
}
