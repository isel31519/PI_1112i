using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PI.CourseAplication.Model
{
    class FucRepository
    {

        private readonly IDictionary<string, Fuc> _repo = new Dictionary<string, Fuc>();
       

        public IEnumerable<Fuc> GetAll()
        {
            return _repo.Values;
        }

        public Fuc GetByAcr(string id)
        {
            Fuc td = null;
            _repo.TryGetValue(id, out td);
            return td;
        }

        public void Add(Fuc td)
        {
            _repo.Add(td.Acr, td);
        }
    }
}
