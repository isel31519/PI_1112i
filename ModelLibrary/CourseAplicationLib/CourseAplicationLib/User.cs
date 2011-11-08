namespace CourseAplicationLib
{
    public sealed class User
    {
        public User(string name, string pass, string email, string role)
        {
            Name = name;
            Pass = pass;
            Email = email;
            Role = role;
            Activated = false;
        }

        public User() {}

        public string Name { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Activated { get; set; }
        public bool Match(string pass) { return Pass.CompareTo(pass)==0; }
    }
}
