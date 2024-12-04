using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountsGui
{
    enum ExceptionType {
        ACCOUNT_DOES_NOT_EXIST,
        CREDIT_LIMIT_HAS_BEEN_EXCEEDED,
        NAME_NOT_ASSOCIATED_WITH_ACCOUNT,
        NO_OVERDRAFT_FOR_THIS_ACCOUNT,
        PASSWORD_INCORRECT,
        USER_DOES_NOT_EXIST,
        USER_NOT_LOGGED_IN
    };
    enum AccountType {
        Checking,
        Saving,
        Visa,
        Line_of_credit
    };
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
