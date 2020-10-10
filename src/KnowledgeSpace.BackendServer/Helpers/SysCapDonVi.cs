using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeSpace.BackendServer.Helpers
{
    public class SysCapDonVi
    {
        public SysCapDonVi()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public readonly string Bo = "01";
        public readonly string So = "02";
        public readonly string Phong = "03";
        public readonly string Truong = "04";

        public static readonly List<string> lstCapDonVi = new List<string>() { "01", "02", "03", "04" };

        public const string BO = "01";
        public const string SO = "02";
        public const string PHONG = "03";
        public const string TRUONG = "04";
    }
}