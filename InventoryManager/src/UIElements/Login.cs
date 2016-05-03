using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    class Login
    {
        public bool _authenticated;
        public string _username;
        public string _password;
        public const string USERNAME = "admin";
        public const string PASSWORD = "admin";

        public Login()
        {
            _authenticated = false;
        }

        public bool Authenticated
        {
            get
            {
                return _authenticated;
            }


        }

        public void Try(string st1, string st2)
        {
            _username = st1;
            _password = st2;
            if (_username == USERNAME && _password == PASSWORD)
            {
                _authenticated = true;

            }


        }
    }
}

