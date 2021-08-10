using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbBdgWaitForApprove
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public string Status { get; set; }
        public string DateSend { get; set; }
        public string DateApprove { get; set; }
        public string ApproveName { get; set; }
        public string Remark { get; set; }
    }
}
