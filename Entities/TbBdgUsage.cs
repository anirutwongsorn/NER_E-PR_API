using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbBdgUsage
    {
        public int Items { get; set; }
        public string RequestId { get; set; }
        public string BdgCode { get; set; }
        public string DeptCode { get; set; }
        public decimal? Paid { get; set; }
        public string Ref { get; set; }
        public string Remark { get; set; }
        public string DatePaid { get; set; }
        public int? DateInt { get; set; }
        public string DateRecord { get; set; }
    }
}
