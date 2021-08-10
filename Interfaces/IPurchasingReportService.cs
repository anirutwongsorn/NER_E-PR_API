using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ner_pr_api.Dtos.OutputDtos;

namespace ner_pr_api.Interfaces
{
    public interface IPurchasingReportService
    {
        Task<List<PrPendingDto>> GetPrOverallSummary(string prYY);

        Task<List<PrPendingDto>> GetPrOverallSummaryByDept(string prYY, string deptCode);

        Task<List<PrPendingDto>> GetPrPendingMain(string prYY);

        Task<List<PrPendingDto>> GetPrPendingItem(string prYY);

        Task<List<NerDepartmentDto>> GetNerDepartment(string deptCode = "");
    }
}