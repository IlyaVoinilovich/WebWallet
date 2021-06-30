
using WebWal.ModelsModels;

namespace WebWal.Interface
{
    public interface ICreateTransactionCommandWebWalletApproved
    {
        /// <summary>
        /// Approved investments
        /// </summary>
        CreateTransactionCommand Transaction { get; set; }
    }
}