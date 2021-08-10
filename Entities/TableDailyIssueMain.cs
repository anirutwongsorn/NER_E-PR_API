using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableDailyIssueMain
    {
        public int IssueItems { get; set; }
        public string DeptCode { get; set; }
        public string IssueDetail { get; set; }
        public string Remark { get; set; }
    }
}
