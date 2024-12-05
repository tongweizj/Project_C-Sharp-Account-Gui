using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
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
            if (type != "VS-" && type != "SV-" && type != "CK-")
            {
                throw new ArgumentException("Invalid account type. Use 'VS-', 'SV-', or 'CK-'.");
            }
            this.Number = type + LAST_NUMBER;
            LAST_NUMBER ++;
            this.Balance = balance;
            this.LowestBalance = balance;
        }
        public void Deposit(decimal amount, Person person)
        {

            this.Balance += amount;

            // TODO: check why add this if?
            if (Balance < LowestBalance)
            {
                LowestBalance = Balance;
            }

            this.Transactions.Add(new Transaction(this.Number, amount, person, Utils.Now));

        }
        public void AddUser(Person person)
        {
            this.Users.Add(person);
        }
        public bool IsUser(string name)
        {
            foreach (Person person in this.Users)
            {
                if (person.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public abstract void PrepareMonthlyReport();

        public virtual void OnTransactionOccur(object sender, EventArgs e)
        {
            OnTransaction?.Invoke(sender,e);
        }
        public override string ToString()
        {
            string balance = $"";
            if (Balance > 0)
            {
                balance = $"${Balance}";
            }
            else
            {
                balance = $"-${Math.Abs(Balance)}";
            }
            
            string result = $"{Number} {string.Join(", ", this.Users)} {balance}\n  {String.Join("\n  ", this.Transactions)}";



            return result;

        }
    }
}
