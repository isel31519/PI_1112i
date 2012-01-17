using System.Collections.Generic;
using System.Linq;

namespace CourseAplicationLib
{
    public class FucRepository
    {

        private readonly IDictionary<string, Fuc> _repo = new Dictionary<string, Fuc>();
       

        public IEnumerable<Fuc> GetAll()
        {
            return _repo.Values;
        }

        public string[] GetAllFucNames()
        {
            IEnumerable<Fuc> fucList = GetAll();

            return (from f in fucList select f.Name).ToArray();
        }

        public string[] FindFucName(string inputQuery)
        {
            var fucNames = GetAllFucNames();

            if (string.IsNullOrEmpty(inputQuery))
            {
                return fucNames;
            }
            else
            {
                var items = (from f in fucNames where f.ToLower().Contains(inputQuery.ToLower()) select f).ToArray();
                return items;
            }
        }

        public Fuc GetByAcr(string id)
        {
            Fuc td = null;
            _repo.TryGetValue(id, out td);
            return td;
        }
        public void Remove(string acr)
        {
            _repo.Remove(acr);
        }

        public void Add(Fuc td)
        {
             _repo.Add(td.Acr, td);
        }
    }
}
