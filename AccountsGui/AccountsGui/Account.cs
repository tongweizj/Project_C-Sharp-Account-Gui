using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGui
{
    internal abstract class Account
    {
        private static int LAST_NUMBER = 100000;
        public readonly List<Person> Users = new List<Person>();
        public readonly List<Transaction> Transactions = new List<Transaction>();
        public virtual event EventHandler OnLogin;
        public virtual event EventHandler OnTransaction;

        public String Number { get; }
        public decimal Balance { get; protected set; }
        public decimal LowestBalance { get; protected set; }

        public Account(string type, decimal balance)
        {
            Balance = balance;
            LowestBalance = balance;
            Number = type + LAST_NUMBER;
            LAST_NUMBER++;
        }
    }
}
