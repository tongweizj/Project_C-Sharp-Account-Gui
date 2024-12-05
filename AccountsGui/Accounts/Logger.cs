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
                LoginEvents.Add($"PersonName: {loginArgs.PersonName}, Success:{loginArgs.Success}, Time: {Utils.Now}.");
            }
        }

        public static void TransactionHandler(object sender, EventArgs args)
        {

            TransactionEventArgs transactionArgs = args as TransactionEventArgs;
            if (transactionArgs != null)
            {

                string eventDetails = $"PersonName: {transactionArgs.PersonName}, Amount: {transactionArgs.Amount}, " +
                                      $" Success: {transactionArgs.Success}, Time: {Utils.Now}";

                TransactionEvents.Add(eventDetails);
            }
        }

        public static void ShowLonginEvents(string filename)
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
        public static void ShowTransactionEvents()
        {
            Console.WriteLine($"Current Time:{Utils.Now}");
            int i = 0;
            foreach (string item in TransactionEvents)
            {
                i++;
                Console.WriteLine($"{i}. {item}");
            }
        }
    }
}
