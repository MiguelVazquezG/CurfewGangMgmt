using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GangManagementSystem.DTO
{
    public class GangsPGDto
    {
        public string Gang__ { get; set; }
        public Nullable<int> Gang_Type_Code { get; set; }
        public Nullable<int> Supervisor { get; set; }
        public Nullable<int> Division { get; set; }
        public Nullable<int> Gang_Number { get; set; }
        public Nullable<float> Lunch { get; set; }
        public Nullable<short> Men_Authorized { get; set; }
        public Nullable<System.DateTime> upsize_ts { get; set; }
        public Nullable<int> GangType { get; set; }
        public Nullable<int> RIN { get; set; }
        public Nullable<bool> HardStop { get; set; }
    }
}