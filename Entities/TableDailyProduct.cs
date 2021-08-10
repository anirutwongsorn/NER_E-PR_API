using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableDailyProduct
    {
        public TableDailyProduct()
        {
            TableDailyStockCards = new HashSet<TableDailyStockCard>();
        }

        public int Items { get; set; }
        public string Pcode { get; set; }
        public string Pname { get; set; }
        public string DeptCode { get; set; }
        public string Remark { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual ICollection<TableDailyStockCard> TableDailyStockCards { get; set; }
    }
}
