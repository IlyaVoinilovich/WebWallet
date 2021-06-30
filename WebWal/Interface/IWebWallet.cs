
using WebWal.ModelsModels;

namespace WebWal.Interface
{
    public interface IWebWallet
    {
        public bool TransactionCreate(CreateTransactionCommand command);
    }
}
