using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    internal class Person
    {
        private string Password;
        public event EventHandler OnLogin;
        public String SIN { get; }
        public String Name { get; }
        public bool IsAuthenticated { get; private set; }

        public Person(string name, string sin)
        {
            this.Name = name;
            this.SIN = sin;
            this.Password = sin.Substring(0, 3);
        }
        public void Login(string password)
        {
            // 1. Throw an AccountException object this the if the argument does not match the password
            // 2. If the argument matches the password the IsAuthenticated property is set to true
            // 3. This method does not display anything

            if (Password.Equals(password))
            {
                this.IsAuthenticated = true;
                OnLogin(this, new LoginEventArgs(this.Name, true));
            }
            else
            {
                this.IsAuthenticated = false;
                OnLogin(this, new LoginEventArgs(this.Name, false));
                throw new AccountException(ExceptionType.PASSWORD_INCORRECT);
            }
        }

        public void Logout()
        {
            this.IsAuthenticated = false;
        }

        public override string ToString()
        {
            // It does not take any parameter but returns a string representing the name of the person and if he is authenticated or not.
            String authStr = IsAuthenticated ? " is authenticated!" : "not authenticated!";
            return $"Name: {Name}, {authStr}";
        }
    }
}
