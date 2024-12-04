using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;
using System.IO; //Setp-1
namespace AccountsGui
{
    internal class Logger
    {
        private static List<string>	loginEvents;
        private static List<string> transactionEvents;

        public static void LoginHandler(object sender, EventArgs args)
        {
            LoginEventArgs loginArgs = args as LoginEventArgs;
         //   PersonName(from args);
	        //Success(from args);
         //   Current time(from Utils.Now);

            loginEvents.Add($"PersonName: {loginArgs.PersonName} Success:{loginArgs.Success} Current time: {Utils.Now}.");

        }

        public static void ShowLonginEvents(string filename)
        {
            TextWriter writer = new StreamWriter(filename); // Setp-2
            writer.WriteLine(Utils.Now);
            int i = 0;
            foreach (string item in loginEvents)
            {
                i++;
                writer.WriteLine($"{i}. {item}");
            }

            writer.Close(); // Setp-3

        }
        public static void ShowTransactionEvents()
        {
            Console.WriteLine(Utils.Now);
            int i = 0;
            foreach (string item in transactionEvents)
            {
                i++;
                Console.WriteLine($"{i}. {item}");
            }
    }
}
