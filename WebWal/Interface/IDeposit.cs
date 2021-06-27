﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWal.Models;

namespace WebWal.Interface
{
    public interface IDeposit
    {
        public Task<BalanceInfo> NewDeposit(DepositCommand command,long UserId);
        public  BalanceInfo Deposit(DepositCommand command, UserWallet wallet); 
    }
}
