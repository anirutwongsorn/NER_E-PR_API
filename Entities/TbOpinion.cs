using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbOpinion
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string Note { get; set; }
        public int Scored { get; set; }
        public bool? Isactive { get; set; }
        public DateTime? Created { get; set; }
    }
}
