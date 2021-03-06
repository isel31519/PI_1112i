﻿namespace CourseAplicationLib
{
    public class ResolveUri
    {
        public static string For(Fuc f)
        {
            return string.Format("{0}/{1}", ForFuc(), f.Acr);
        }

        public static string For(FucProposal f)
        {
            return string.Format("{0}/{1}{2}/{3}", ForFuc(), f.Acr, ForProp(), f.Idx);
        }

        private static string ForProp()
        {
           return "/prop";
        }

        public static string ForNew(FucProposal f)
        {
            return string.Format("{0}/{1}",ForNewFuc(), f.Idx);
        }

        private static string ForNewFuc()
        {
            return "/newfuc";
        }

        public static string ForCreate()
        {
            return "/create";
        }

        public static string ForFuc()
        {
            return "/fuc";
        }

        public static string ForRoot()
        {
            return "/";
        }
    }
}
