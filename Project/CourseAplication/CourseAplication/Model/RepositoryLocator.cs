using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseAplication.Model
{
    class RepositoryLocator
    {
        private readonly static FucRepository FucRepo = new FucRepository();
        private readonly static ProposalRepository PropRepo = new ProposalRepository();
        private readonly static ProposalRepository NewPropRepo = new ProposalRepository();
        private readonly static UserRepository UserRepo = new UserRepository();
        public static FucRepository GetFucRep()
        {
            return FucRepo;
        }

        public static ProposalRepository GetPropRep()
        {
            return PropRepo;
        }
        public static ProposalRepository GetNewPropRep()
        {
            return NewPropRepo;
        }

        public static UserRepository GetUserRep()
        {
            return UserRepo;
        }
    }
}
