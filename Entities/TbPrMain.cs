using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbPrMain
    {
        public TbPrMain()
        {
            TbPrMainDetails = new HashSet<TbPrMainDetail>();
        }

        public int Items { get; set; }
        public string PrNo { get; set; }
        public string UserId { get; set; }
        public string DeptCode { get; set; }
        public string DateCreated { get; set; }
        public string TimeCreated { get; set; }
        public string PrType { get; set; }
        public string DateNeed { get; set; }
        public string Remark { get; set; }
        public int? Rev { get; set; }
        public string ApproveDate { get; set; }
        public string ApproveName { get; set; }
        public string ApproveTime { get; set; }
        public string ApproveRemark { get; set; }
        public DateTime? AcknDate { get; set; }
        public string AcknName { get; set; }
        public bool? IsAckn { get; set; }
        public string OperateDate { get; set; }
        public string OperatelName { get; set; }
        public string OperatelRemark { get; set; }
        public int? DateInt { get; set; }
        public string BudgetsStatus { get; set; }
        public string Status { get; set; }
        public DateTime? DateNow { get; set; }

        public virtual TbUser User { get; set; }
        public virtual ICollection<TbPrMainDetail> TbPrMainDetails { get; set; }
    }
}
