using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbPrTimeSetting
    {
        public int Id { get; set; }
        public DateTime? InitTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
