using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseAplicationLib;

namespace CourseAplicationMVC.Ordenation
{
    public abstract class ProposalOrdenation : IOrdenation<FucProposal>
    {
        protected FucProposal[] array;
        protected ProposalOrdenation(FucProposal[] ar)
        {
            array = ar;
        }
        public abstract IEnumerable<FucProposal> Order(ResolveOrdenationType.OrderType type);
    }

    public class ProposalCreatorOrdenation : ProposalOrdenation
    {

        public ProposalCreatorOrdenation(FucProposal[] ar)
            : base(ar)
        {
        }
        public override IEnumerable<FucProposal> Order(ResolveOrdenationType.OrderType type)
        {
            return type == ResolveOrdenationType.OrderType.Ascending ? array.OrderBy(n => n.Idx).ToList() :
                 array.OrderByDescending(n => n.Idx).ToList();  
        }
    }

    public class ProposalAcronymOrdenation : ProposalOrdenation
    {
        public ProposalAcronymOrdenation(FucProposal[] ar)
            : base(ar)
        {
        }
        public override IEnumerable<FucProposal> Order(ResolveOrdenationType.OrderType type)
        {
            return type == ResolveOrdenationType.OrderType.Ascending ? array.OrderBy(n => n.Acr).ToList() :
                 array.OrderByDescending(n => n.Acr).ToList();  
        }
    }

    public class ProposalNameOrdenation : ProposalOrdenation
    {
        public ProposalNameOrdenation(FucProposal[] ar)
            : base(ar)
        {
        }
        public override IEnumerable<FucProposal> Order(ResolveOrdenationType.OrderType type)
        {
            return type == ResolveOrdenationType.OrderType.Ascending ? array.OrderBy(n => n.User).ToList() :
                 array.OrderByDescending(n => n.User).ToList();  
        }
    }
}