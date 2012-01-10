using System.Collections.Generic;

namespace CourseAplicationLib
{
    public class UserRepository
    {
        private readonly IDictionary<string, User> _repo = new Dictionary<string, User>();

        public IEnumerable<User> GetAll()
        {
            return _repo.Values;
        }

        public User GetById(string id)
        {
            User td = null;
            _repo.TryGetValue(id, out td);
            return td;
        }

        public void Add(User td)
        {
            _repo.Add(td.Name, td);
        }
        /*
        public void Remove(int id)
        {
            _repo.Remove(id);
        }

        public void Edit(int id, FucProposal f)
        {
            f.Id = id;
            _repo.Remove(id);
            _repo.Add(id, f);
        }*/
    }
}
