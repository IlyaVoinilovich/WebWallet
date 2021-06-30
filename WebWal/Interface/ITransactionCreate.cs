

using WebWal.ModelsModels;

namespace WebWal.Interface
{
    public interface ITransactionCreate
    {
        CreateTransactionCommand Transaction { get; set; }
    }
}
