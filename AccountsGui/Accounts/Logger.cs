using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    internal class Logger
    {
        private static List<string> LoginEvents = new List<string>();
        private static List<string> TransactionEvents = new List<string>();

        public static void LoginHandler(object sender, EventArgs args)
        {
            LoginEventArgs loginArgs = args as LoginEventArgs;
            //   PersonName(from args);
            //Success(from args);
            //   Current time(from Utils.Now);
            if (loginArgs != null)
            {
                string info = loginArgs.Success ? "successfully" : "unsuccessfully";
                LoginEvents.Add($"{loginArgs.PersonName} logged in s{info} on {Utils.Now}");
            }
        }

        public static void TransactionHandler(object sender, EventArgs args)
        {

            TransactionEventArgs transactionArgs = args as TransactionEventArgs;
            if (transactionArgs != null)
            {
                //1 Narendra deposit $1,500.00 successfully on 2021-08-12 11:59
                string successInfo = transactionArgs.Success ? "successfully" : "unsuccessfully";
                //string amountInfo = (transactionArgs.Amount>0) ? "deposit" : "unsuccessfully";
                string eventDetails = $"{transactionArgs.PersonName} deposit {transactionArgs.Amount} {successInfo} on {Utils.Now}";

                TransactionEvents.Add(eventDetails);
            }
        }

        public static void SaveLoginEvents(string filename)
        {
            StreamWriter writer = new StreamWriter(filename); // Setp-2
            writer.WriteLine($"Current Time:{Utils.Now}");
            int i = 0;
            foreach (string item in LoginEvents)
            {
                i++;
                writer.WriteLine($"{i}. {item}");
            }

            writer.Close(); // Setp-3

        }

        public static void ShowLoginEvents()
        {
            Console.WriteLine($"Current Time:{Utils.Now}");
            int i = 0;
            foreach (string item in LoginEvents)
            {
                i++;
                Console.WriteLine($"{i} {item}");
            }
        }
        public static void ShowTransactionEvents()
        {
            Console.WriteLine($"Current Time:{Utils.Now}");
            int i = 0;
            foreach (string item in TransactionEvents)
            {
                i++;
                Console.WriteLine($"{i} {item}");
            }
        }
    }
}
