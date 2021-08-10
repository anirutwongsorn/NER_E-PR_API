using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableHrLeave
    {
        public int Items { get; set; }
        public string DocNo { get; set; }
        public string TypeLeave { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string DeptCode { get; set; }
        public string Division { get; set; }
        public int? DateInt { get; set; }
        public string DateNow { get; set; }
        public string DateLeaveFrom { get; set; }
        public string DateLeaveEnd { get; set; }
        public decimal? Qty { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
    }
}
