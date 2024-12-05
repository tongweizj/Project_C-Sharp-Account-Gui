using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
namespace Accounts
{
    internal static class Bank
    {
        public static readonly  Dictionary<string, Account> Accounts = new Dictionary<string, Account>();
        public static readonly Dictionary<string, Person> Users = new Dictionary<string, Person>();

        static Bank()
        {
            Initialize();
        }


        static void Initialize()
        {
            //initialize the USERS collection
            AddUser("Narendra", "1234-5678");    //0
            AddUser("Ilia", "2345-6789");        //1
            AddUser("Mehrdad", "3456-7890");     //2
            AddUser("Vinay", "4567-8901");       //3
            AddUser("Arben", "5678-9012");       //4
            AddUser("Patrick", "6789-0123");     //5
            AddUser("Yin", "7890-1234");         //6
            AddUser("Hao", "8901-2345");         //7
            AddUser("Jake", "9012-3456");        //8
            AddUser("Mayy", "1224-5678");        //9
            AddUser("Nicoletta", "2344-6789");   //10


            //initialize the ACCOUNTS collection
            AddAccount(new VisaAccount());              //VS-100000
            AddAccount(new VisaAccount(150, -500));     //VS-100001
            AddAccount(new SavingAccount(5000));        //SV-100002
            AddAccount(new SavingAccount());            //SV-100003
            AddAccount(new CheckingAccount(2000));      //CK-100004
            AddAccount(new CheckingAccount(1500, true));//CK-100005
            AddAccount(new VisaAccount(50, -550));      //VS-100006
            AddAccount(new SavingAccount(1000));        //SV-100007 

            //associate users with accounts
            string number = "VS-100000";
            AddUserToAccount(number, "Narendra");
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Mehrdad");

            number = "VS-100001";
            AddUserToAccount(number, "Vinay");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Patrick");

            number = "SV-100002";
            AddUserToAccount(number, "Yin");
            AddUserToAccount(number, "Hao");
            AddUserToAccount(number, "Jake");

            number = "SV-100003";
            AddUserToAccount(number, "Mayy");
            AddUserToAccount(number, "Nicoletta");

            number = "CK-100004";
            AddUserToAccount(number, "Mehrdad");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Yin");

            number = "CK-100005";
            AddUserToAccount(number, "Jake");
            AddUserToAccount(number, "Nicoletta");

            number = "VS-100006";
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Vinay");

            number = "SV-100007";
            AddUserToAccount(number, "Patrick");
            AddUserToAccount(number, "Hao");

        }

        public static void PrintAccounts()
        {
            foreach (var entry in Accounts)
            {
                Console.WriteLine(entry.ToString());
            }
        }


        public static void SaveAccounts(string filename)
        {
            //Create and initialise a serializer object
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //saves the json string to the file
            File.WriteAllText(filename, serializer.Serialize(Accounts));
        }
        public static void SaveUsers(string filename) {
            //Create and initialise a serializer object
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //saves the json string to the file
            File.WriteAllText(filename, serializer.Serialize(Users));
        }
        //public static void PrintPersons()
        //{
        //    Console.WriteLine(String.Join("\n", Users));
        //}

        public static Person GetUser(string name)
        {
            try
            {
            if (Users.TryGetValue(name, out Person person))
            {
                return person;
            }
            else { 
                throw new AccountException(ExceptionType.USER_DOES_NOT_EXIST);
            }
            }
            catch (AccountException ex)
            {
                Console.WriteLine("AccountException：" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SystemException：" + ex.Message);
            }
            return null;
        }

        public static Account GetAccount(string number)
        {
            try
            {
                if (Accounts.TryGetValue(number, out Account account))
                {
                    return account;
                }
                else
                {
                    throw new AccountException(ExceptionType.ACCOUNT_DOES_NOT_EXIST);
                }
            }
            catch (AccountException ex)
            {
                Console.WriteLine("AccountException：" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SystemException：" + ex.Message);
            }
            return null;
        }

        public static void AddUser(string name, string sin) {
            // a.Creates a Person object with the two arguments.
            // b.Add the static method LoginHandler() of the Logger class to the Eventhandler field(OnLogin) of the above object.
            // c.Adds a key-value pair to the USERS dictionary.The key is the second argument of the method and the value is the object created in step a.

            Person person = new Person(name,sin);
            person.OnLogin += Logger.LoginHandler ;
            Users[name] = person;
        }
        public static void AddAccount(Account account) {
            //a.Add the static method TransactionHandler() of the Logger class to the Eventhandler field(OnTransaction) of the argument.
            //b.Adds a key-value pair to the ACCOUNTS collection.The key is the Number property of the argument and the value is the actual argument respectively.

            account.OnTransaction += Logger.TransactionHandler;
            Accounts[account.Number] = account;
        }
        public static void AddUserToAccount(string number, string name)
        {
            // this method takes two string arguments and does the following:
            // a.Locates the account matching the first argument.
            Account account = GetAccount(number);

            // b.Locates the person matching the second argument.
            Person person = GetUser(name);
            // c.Invokes the AddUser() method on the account object and passing the person object.
            account.AddUser(person);
        }
        public static List<Transaction> GetAllTransactions() {
            List<Transaction> transactions = new List<Transaction>();
            foreach (var entry in Accounts)
            {
                transactions.Concat(entry.Value.Transactions).ToList<Transaction>();
                // do something with entry.Value or entry.Key
            }
            return transactions;
        }
    }
}
