using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableVehOrder
    {
        public int Items { get; set; }
        public string OrderId { get; set; }
        public string DateNow { get; set; }
        public string DeptCode { get; set; }
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Position { get; set; }
        public string Division { get; set; }
        public string Reason { get; set; }
        public string DateOrder { get; set; }
        public string TimeOrder { get; set; }
        public string Priority { get; set; }
        public string Vehicle { get; set; }
        public string LicensePlate { get; set; }
        public string Driver { get; set; }
        public string Remark { get; set; }
        public string DateApprove { get; set; }
        public string Checker { get; set; }
        public int? DateInt { get; set; }
        public string Status { get; set; }
    }
}
