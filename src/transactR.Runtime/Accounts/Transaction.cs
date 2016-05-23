﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactR.Core.Entities;
using transactR.Core.Utilities;

namespace transactR.Runtime.Accounts
{
    public class Transaction:Entity
    {
        public Transaction()
        {
            ActionDate = SessionState.Current.ActionDate;
            ValueDate = SessionState.Current.ValueDate;
        }

        public virtual Account Account { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime ActionDate { get; set; }
        public virtual DateTime ValueDate { get; set; }
        public virtual string TransactionType { get; set; }
    }
}
