using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableItRequestDomain
    {
        public int Items { get; set; }
        public string UserId { get; set; }
        public string FullNameTh { get; set; }
        public string FullNameEn { get; set; }
        public string UserPosition { get; set; }
        public string DeptSymbol { get; set; }
        public string Division { get; set; }
        public string Domain { get; set; }
        public string Pass { get; set; }
        public string DateRequest { get; set; }
        public int? DateInt { get; set; }
        public string CheckedName { get; set; }
        public string Status { get; set; }
    }
}
