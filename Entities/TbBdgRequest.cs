using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbBdgRequest
    {
        public int Items { get; set; }
        public string RequestId { get; set; }
        public string BdgCode { get; set; }
        public string DeptCode { get; set; }
        public decimal? MoneyRequest { get; set; }
        public string DateRequest { get; set; }
        public string Yearly { get; set; }
    }
}
