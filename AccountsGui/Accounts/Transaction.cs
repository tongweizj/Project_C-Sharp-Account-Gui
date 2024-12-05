using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    internal class Transaction
    {
        public string AccountNumber { get; }
        public decimal Amount { get; }
        public Person Originator { get; }
        public DayTime Time { get; }

        public Transaction(string accountNumber, decimal amount, Person person, DayTime time)
        {
            this.AccountNumber = accountNumber;
            this.Amount = amount;
            this.Originator = person;
            this.Time = time;
        }

        public override string ToString()
        {
            string type = this.Amount>0? "deposited" : "withdrawn";
            return $"{AccountNumber} ${Math.Abs(Amount)} {type} by {Originator} on {Time}";
        }
    }
}
