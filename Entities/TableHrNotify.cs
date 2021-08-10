using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableHrNotify
    {
        public int Item { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Date { get; set; }
        public int? DateInt { get; set; }
        public string FullName { get; set; }
    }
}
