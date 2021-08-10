using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class ViewPr
    {
        public string PrNo { get; set; }
        public string DeptSymbol { get; set; }
        public string Division { get; set; }
        public string DateCreated { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string PdCode { get; set; }
        public string PdDetail { get; set; }
        public string Qty { get; set; }
        public string Keeping { get; set; }
        public string BdgCode { get; set; }
        public string Objective { get; set; }
        public string Po { get; set; }
    }
}
