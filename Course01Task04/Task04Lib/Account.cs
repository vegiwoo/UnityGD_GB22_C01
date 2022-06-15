using System;

namespace Task04Lib 
{
    [Serializable]
    public struct Account {
        public string Login { get; set; }
        public string Password { get; set;}

        public Account(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return $"{Login} {Password}";
        }
    }
}