using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GangManagementSystem.DTO
{
    public class GangTypePGDto
    {
        public int Gang_Type_Code { get; set; }
        public string Gang_Type1 { get; set; }
        public string Gang_Prefix { get; set; }
        public Nullable<double> MPVariance { get; set; }
        public Nullable<int> Order { get; set; }
    }
}