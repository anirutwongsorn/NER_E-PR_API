using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableDailyStockCard
    {
        public int StkId { get; set; }
        public string RptId { get; set; }
        public string Pcode { get; set; }
        public decimal? OlderQty { get; set; }
        public decimal? RetrieveQty { get; set; }
        public decimal? IssueQty { get; set; }
        public int? KeepIdOlder { get; set; }
        public int? KeepIdReceive { get; set; }
        public int? KeepIdIssue { get; set; }
        public string Remark { get; set; }
        public bool? Active { get; set; }

        public virtual TableDailyProduct PcodeNavigation { get; set; }
        public virtual TableDailyStockMain Rpt { get; set; }
    }
}
