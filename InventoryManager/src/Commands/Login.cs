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
            //authenticated set to false by default for error handling
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

            //match username and password to constant
            if (_username == USERNAME && _password == PASSWORD)
            {
                //if match found, return true
                _authenticated = true;

            }


        }
    }
}

