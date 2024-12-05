using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    internal class VisaAccount : Account
    {

        private static decimal INTEREST_RATE = 0.1995M;
        private decimal CredilLimit;
        public VisaAccount(decimal balance = 0, decimal creditLimit = 1200) : base("VS-", balance)
        {
            this.CredilLimit = creditLimit;
        }
        public void DoPayment(decimal amount, Person person)
        {
            base.Deposit(amount, person);
            base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
              , true));
        }

        public void DoPurchase(decimal amount, Person person)
        {
            if (!base.IsUser(person.Name))
            {
                base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
              , false));
                throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
            }
            if (!person.IsAuthenticated)
            {
                base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
             , false));
                throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
            }
            if ((base.Balance - amount) < CredilLimit)
            {
                base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
             , false));
                throw new AccountException(ExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
            }

            base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
              , true));
            base.Deposit(amount * (-1), person);



        }
        public override void PrepareMonthlyReport()
        {
            base.Balance -= base.LowestBalance * INTEREST_RATE / 12;

            base.Transactions.Clear();
        }
        //public double GetCredilLimit()
        //{
        //    return this.CredilLimit;
        //}
    }
}
