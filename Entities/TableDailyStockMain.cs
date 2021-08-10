using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableDailyStockMain
    {
        public TableDailyStockMain()
        {
            TableDailyStockCards = new HashSet<TableDailyStockCard>();
        }

        public int Items { get; set; }
        public string RptId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? CheckedDate { get; set; }
        public string CheckedName { get; set; }
        public string DeptCode { get; set; }
        public string UserId { get; set; }
        public string Remark { get; set; }
        public bool? Rptstatus { get; set; }
        public bool? Isactive { get; set; }

        public virtual ICollection<TableDailyStockCard> TableDailyStockCards { get; set; }
    }
}
