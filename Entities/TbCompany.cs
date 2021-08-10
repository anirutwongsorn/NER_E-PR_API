using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbCompany
    {
        public int Items { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
    }
}
