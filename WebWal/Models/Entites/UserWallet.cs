
using System;
using System.ComponentModel.DataAnnotations;
using WebWal.Helpers;

namespace WebWal.Models
{
    public class UserWallet
    {
        public UserWallet(decimal balance, Currency currency, long userId)
        {
            Balance = balance;
            Currency = currency;
            UserId = userId;
        }

        [Key]
        public long Id { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
        public long UserId { get; set; }
        public void AddBalance(decimal balance)
        {
            if (balance <= 0) throw new ArgumentOutOfRangeException(nameof(balance));
            Balance += balance;
        }
        public void SubtractBalance(decimal balance)
        {
            if (balance <= 0) throw new ArgumentOutOfRangeException(nameof(balance));
            Balance -= balance;
        }
    }
}
