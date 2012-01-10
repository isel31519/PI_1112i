using System.Collections.Generic;

namespace CourseAplicationLib
{
    public class ProposalRepository
    {
        private readonly IDictionary<int, FucProposal> _repo = new Dictionary<int, FucProposal>();
        private int _cid = 0;

        public IEnumerable<FucProposal> GetAll()
        {
            return _repo.Values;
        }

        public FucProposal GetById(int id)
        {
            FucProposal td = null;
            _repo.TryGetValue(id, out td);
            return td;
        }

        public void Add(FucProposal td)
        {
            td.Idx = _cid;
            _repo.Add(_cid++, td);
        }

        public void Remove(int id)
        {
            _repo.Remove(id);
        }

        public void Edit(int id, FucProposal f)
        {
            f.Idx = id;
            _repo.Remove(id);
            _repo.Add(id,f);
        }

        internal bool HaveProp(string userid,string acr)
        {
            foreach(var f in GetAll())
            {
                if (f.Acr.Equals(acr) && f.User.Equals(userid)) return true;
            }
            return false;
        }

        public IEnumerable<FucProposal> GetByUser(string username)
        {
            foreach (var f in GetAll())
                if (f.User.Equals(username)) yield return f;
        }
    }
}
