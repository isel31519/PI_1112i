using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseAplication.Model
{
    class ProposalRepository
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
            td.Id = _cid;
            _repo.Add(_cid++, td);
        }
    }
}
