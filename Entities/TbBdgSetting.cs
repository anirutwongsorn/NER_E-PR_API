using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbBdgSetting
    {
        public int Items { get; set; }
        public string Yearly { get; set; }
        public string TitleMessage { get; set; }
        public int? Status { get; set; }
    }
}
