using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableHrCvMain
    {
        public int Items { get; set; }
        public string CvId { get; set; }
        public string DeptCode { get; set; }
        public string Room { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Detail { get; set; }
        public int? QtyPerson { get; set; }
        public int? DateInt { get; set; }
        public string DateRequest { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Remark { get; set; }
        public string Checker { get; set; }
        public string Status { get; set; }
    }
}
