using System;
using ner_api.Entities;

namespace ner_pr_api.Dtos.OutputDtos
{
    public class PurchasingDescDto
    {
        public string PrNo { get; set; }
        public string PrType { get; set; }
        public string PrStatus { get; set; }
        public string DeptCode { get; set; }
        public string DeptSymbol { get; set; }
        public string Division { get; set; }
        public string FullName { get; set; }
        public string DateCreated { get; set; }
        public string TimeCreated { get; set; }
        public string Remark { get; set; }
        public string ApproveDate { get; set; }
        public string ApproveTime { get; set; }
        public string ApproveName { get; set; }
        public string BudgetsStatus { get; set; }
        public int Item { get; set; }
        public string PdCode { get; set; }
        public string PdDetail { get; set; }
        public string Qty { get; set; }
        public string Keeping { get; set; }
        public string BdgCode { get; set; }
        public string Objective { get; set; }
        public string Po { get; set; }
        public string PoDate { get; set; }
        public bool ApproveStatus { get; set; }
        public bool ItemsStatus { get; set; }
        //public DateTime Podate { get; set; }

        public static PurchasingDescDto fromTbPrMainDetail(TbPrMainDetail model) => new PurchasingDescDto
        {
            Item = model.Item,
            PrNo = model.PrNo,
            PdCode = model.PdCode,
            PdDetail = model.PdDetail,
            Qty = model.Qty,
            Keeping = model.Keeping,
            BdgCode = model.BdgCode,
            Objective = model.Objective,
            Po = model.Po,
            ApproveStatus = bool.Parse(model.ApproveStatus.ToString()),
            ItemsStatus = bool.Parse(model.ItemsStatus.ToString()),
            PoDate = model.Podate.ToString(),
        };

    }
}