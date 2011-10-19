using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseAplication.Model
{
    sealed class User
    {
        //private int _id;
        private string _username;
        private string _password;

        public User(string name,string pass)
        {
            _username = name;
            _password = pass;
        }

        //public int Id { get { return _id; } set { _id = value; } }
        public string Name {get {return _username;}}
        public string Pass { get { return _password; } }
        public bool Match(string pass)
        {
            return _password.CompareTo(pass)==0;
        }
    }
}
