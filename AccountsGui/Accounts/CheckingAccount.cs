using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    internal class CheckingAccount : Account
    {
        private static decimal COST_PER_TRANSACTION = 0.05M;
        private static decimal INTEREST_RATE = 0.005M;
        private bool hasOverdraft;

        public CheckingAccount(decimal balance = 0, bool hasOverdraft = false) : base("CK-", balance)
        {
            this.hasOverdraft = hasOverdraft;
        }
        public new void Deposit(decimal amount, Person person)
        {
            base.Deposit(amount, person);
            base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
                , true));
        }
        public void Withdraw(decimal amount, Person person)
        {
            try
            {
                if (!base.IsUser(person.Name))
                {
                    base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
               , false));
                    throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT); //"NAME_NOT_ASSOCIATED_WITH_ACCOUN."
                }
                if (!person.IsAuthenticated)
                {
                    base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
              , false));
                    throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);//"USER_NOT_LOGGED_IN"
                }
                if (amount > base.Balance && !hasOverdraft)
                {
                    base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
              , false));
                    throw new AccountException(ExceptionType.NO_OVERDRAFT_FOR_THIS_ACCOUNT);
                }

                base.OnTransactionOccur(person, new TransactionEventArgs(person.Name, amount
              , true));
                base.Deposit(amount * (-1), person);
            }
            catch (AccountException ex)
            {
                Console.WriteLine( ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message);
            }
        }
        public override void PrepareMonthlyReport()
        {
            decimal serviceCharge = COST_PER_TRANSACTION * base.Transactions.Count;
            decimal interest = (base.LowestBalance * INTEREST_RATE) / 12;
            //Console.WriteLine($"{serviceCharge} - {interest}");
            base.Balance = base.Balance + interest - serviceCharge;

            base.Transactions.Clear();
        }



    }
}
