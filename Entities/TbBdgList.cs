using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbBdgList
    {
        public int Items { get; set; }
        public string BdgCode { get; set; }
        public string TypeCode { get; set; }
        public string BdgDetail { get; set; }
        public double? Status { get; set; }
    }
}
