using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbPrMainDetail
    {
        public int Item { get; set; }
        public string PrNo { get; set; }
        public string PdCode { get; set; }
        public string PdDetail { get; set; }
        public string Qty { get; set; }
        public string Keeping { get; set; }
        public string BdgCode { get; set; }
        public string Objective { get; set; }
        public string Po { get; set; }
        public bool? ApproveStatus { get; set; }
        public bool? ItemsStatus { get; set; }
        public DateTime? Podate { get; set; }

        public virtual TbPrMain PrNoNavigation { get; set; }
    }
}
