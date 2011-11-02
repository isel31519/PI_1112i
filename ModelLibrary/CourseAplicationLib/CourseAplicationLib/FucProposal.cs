namespace CourseAplicationLib
{
    public class FucProposal:Fuc
    {
        public FucProposal() {}

        public FucProposal(string user)
        {
            User = user;
        }

        public int Id { get; set; }
        public string User { get; set; }
    }
}
