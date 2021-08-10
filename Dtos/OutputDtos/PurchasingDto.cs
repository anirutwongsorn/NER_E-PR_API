using System;
using ner_api.Entities;
using System.Globalization;

namespace ner_pr_api.Dtos.OutputDtos
{
    public class PurchasingDto
    {

        public string PrNo { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string DeptCode { get; set; }
        public string DateCreated { get; set; }
        public string TimeCreated { get; set; }
        public string PrType { get; set; }
        public string DateNeed { get; set; }
        public string Remark { get; set; }
        public string ApproveDate { get; set; }
        public string ApproveName { get; set; }
        public string ApproveTime { get; set; }
        public string ApproveRemark { get; set; }
        public string OperateDate { get; set; }
        public string OperatelName { get; set; }
        public string OperatelRemark { get; set; }
        public string BudgetsStatus { get; set; }
        public string Status { get; set; }
        public int ItemCount { get; set; }
        public string AcknDate { get; set; }
        public string AcknName { get; set; }
        public bool IsAckn { get; set; }
        public DateTime? DateNow { get; set; }

        public static PurchasingDto FromPrMain(TbPrMain model) => new PurchasingDto
        {
            PrNo = model.PrNo,
            UserId = model.UserId,
            FullName = model.User.FullName,
            DeptCode = model.DeptCode,
            DateCreated = model.DateCreated,
            TimeCreated = model.TimeCreated,
            PrType = model.PrType,
            DateNeed = model.DateNeed,
            Remark = model.Remark,
            ApproveDate = model.ApproveDate == null ? "-" : model.ApproveDate,
            ApproveName = model.ApproveName == null ? "-" : model.ApproveName,
            ApproveTime = model.ApproveTime == null ? "-" : model.ApproveTime,
            ApproveRemark = model.ApproveRemark,
            OperateDate = model.OperateDate == null ? "-" : model.OperateDate,
            OperatelName = model.OperatelName == null ? "-" : model.OperatelName,
            OperatelRemark = model.OperatelRemark == null ? "-" : model.OperatelRemark,
            BudgetsStatus = model.BudgetsStatus == null ? "-" : model.BudgetsStatus,
            Status = model.Status,
            ItemCount = model.TbPrMainDetails.Count,
            AcknDate = model.IsAckn == false ? "-" : DateTime.Parse(model.AcknDate.ToString()).ToString("dd-MM-yyyy", new CultureInfo("th-TH")),
            AcknName = model.AcknName == null ? "-" : model.AcknName,
            IsAckn = model.IsAckn == null ? false : bool.Parse(model.IsAckn.ToString()),
            DateNow = model.DateNow
        };
    }
}