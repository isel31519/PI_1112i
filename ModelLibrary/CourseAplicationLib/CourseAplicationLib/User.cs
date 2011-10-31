namespace CourseAplicationLib
{
    public sealed class User
    {
        //private int _id;
        private string _username;
        private string _password;
        private string _role;

        public User(string name,string pass,string role)
        {
            _username = name;
            _password = pass;
            _role = role;
        }

        public User(string name, string pass)
        {
            _username = name;
            _password = pass;
            _role = null;
        }

        //public int Id { get { return _id; } set { _id = value; } }
        public string Name {get {return _username;}}
        public string Role { get { return _role; } }
        public string Pass { get { return _password; } }
        public bool Match(string pass)
        {
            return _password.CompareTo(pass)==0;
        }
    }
}
