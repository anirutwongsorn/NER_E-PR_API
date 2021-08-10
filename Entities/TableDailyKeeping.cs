using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableDailyKeeping
    {
        public int KeepId { get; set; }
        public string DeptCode { get; set; }
        public string Keeping { get; set; }
        public decimal? Multiplier { get; set; }
        public string Remark { get; set; }
    }
}
