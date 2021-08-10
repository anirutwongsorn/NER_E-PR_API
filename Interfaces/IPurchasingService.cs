using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ner_api.Entities;
using ner_pr_api.Dtos.OutputDtos;

namespace ner_pr_api.Interfaces
{
    public interface IPurchasingService
    {
        Task<List<PurchasingDto>> GetPurchasingMainByStatus(string status = "checked");

        Task<List<PurchasingDescDto>> GetPurchasingItemByStatus(string status = "checked");

        Task<List<PurchasingDescDto>> GetPurchasingItemByDate(string dFrom, string dTo);

        Task<List<TbPrMain>> GetPurchasingByStatus(string prNo = "", string status = "checked");

        Task<List<TbPrMain>> GetPurchasingByDocNo(string prNo);

        Task<List<PurchasingDto>> GetPurchasingByDeptCode(string prYear, string deptCode, string status = "checked");

        Task<List<PurchasingDescDto>> GetPurchasingDescByDocNo(string PrNo);

        Task<List<TbPrMain>> GetPurchasingByDate(DateTime dFrom, DateTime dTo);

        Task<TimeSettingDto> GetTimePeriodSetting();

        Task<int> AcknowledgePr(string prNo = "", string name = "");

        Task<int> UpdatePrStatus(string prNo, string oldStatus, string newStatus, string operationName);

        Task<int> UpdatePrItemStatus(int item, string poNo, string poDate, bool isAppr);

        Task<int> UpdateTimeLimit(DateTime dFrom, DateTime dTo);
    }
}