using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGui
{
    //internal class Transaction
    //{
    //}


    internal struct Transaction
    {
        public string AccountNumber { get; }
        public decimal Amount { get; }
        public Person Originator { get; }
        public DayTime Time { get; }

        public Transaction(string accountNumber, decimal amount,  Person person, DayTime time )
        {
            this.AccountNumber = accountNumber;
            this.Amount = amount;
            this.Originator = person;
            this.Time = time;
        }

       public override string ToString()
        {
            return $"{AccountNumber} ${Amount} withdrawn by {Originator} on {Time}";
        }
    }
}
