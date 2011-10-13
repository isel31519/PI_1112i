using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseAplication.Model
{
    class FucRepositoryLocator
    {
        private readonly static FucRepository Repo = new FucRepository();
        public static FucRepository Get()
        {
            return Repo;
        }
    }
}
