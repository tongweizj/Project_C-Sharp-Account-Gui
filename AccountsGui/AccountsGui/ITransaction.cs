using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsGui
{
    internal interface ITransaction
    {
         void Withdraw(amount : decimal, person : Person);
        void Deposit(amount : decimal, person : Person);

    }
}
