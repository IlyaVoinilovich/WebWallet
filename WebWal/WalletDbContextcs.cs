using Microsoft.EntityFrameworkCore;
using WebWal.Models;

namespace WebWal
{
    public class WalletDbContextcs: DbContext
    {
        public WalletDbContextcs(DbContextOptions<WalletDbContextcs> options) : base(options)
        {
            if (Database.CanConnect() == false)//проверка существует ли бд 
                Database.EnsureCreated();   // создаем бд с новой схемой
        }
        public DbSet<UserWallet> Wallets { get; set; }
    }
}
