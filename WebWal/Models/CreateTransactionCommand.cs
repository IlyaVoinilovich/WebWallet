using System;

namespace WebWal.ModelsModels
{
    public class CreateTransactionCommand
    {
        public Guid Id { get; set; }
        public DateTime date { get; set; }
        public long idUser { get; set; }
        public decimal balance { get; set; }
        public string currency { get; set; }

    }
}
