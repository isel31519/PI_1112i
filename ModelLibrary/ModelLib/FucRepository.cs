﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseAplicationLib
{
    public class FucRepository
    {
        private int _n;

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

        public IEnumerable<Fuc> GetPartialFucs(int n)
        {
            Fuc[] fucList = GetAll().ToArray();
            LinkedList<Fuc> loadFucs = new LinkedList<Fuc>();
            int max = _n + n;
            Fuc f;

            for(;_n<max && _n<fucList.Length ;_n++)
            {
                f = new Fuc(fucList[_n].Name, fucList[_n].Acr);
                loadFucs.AddLast(f);
            }
            return loadFucs;
        }

        public IEnumerable<Fuc> GetPaged(int? page, int? itemsnumber)
        {

            Fuc[] array=_repo.Values.ToArray();
            int max_elem = Math.Min((int)(page * itemsnumber), _repo.Count);
            LinkedList<Fuc> list=new LinkedList<Fuc>();

            for (int i = (int)((page - 1) * itemsnumber), j = 0; i < max_elem; j++, i++)
            {
                list.AddLast(array[i]);
            }

            return list;
        }
    }
}
