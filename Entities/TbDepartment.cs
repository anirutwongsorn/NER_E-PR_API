using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbDepartment
    {
        public int Items { get; set; }
        public string DeptCode { get; set; }
        public string DeptSymbol { get; set; }
        public string Division { get; set; }
        public string CompanyCode { get; set; }
        public string Remark { get; set; }
        public int? Status { get; set; }
    }
}
