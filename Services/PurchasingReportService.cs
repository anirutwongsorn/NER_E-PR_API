using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ner_api.Data;
using ner_pr_api.Dtos.OutputDtos;
using ner_pr_api.Interfaces;

namespace ner_pr_api.Services
{
    public class PurchasingReportService : IPurchasingReportService
    {
        private List<PrPendingDto> PrPendingModel;
        private readonly DatabaseContext dbContext;
        private readonly IConfiguration configration;

        public PurchasingReportService(DatabaseContext dbContext, IConfiguration configration)
        {
            this.configration = configration;
            this.dbContext = dbContext;
            PrPendingModel = new List<PrPendingDto>();
        }

        public async Task<List<PrPendingDto>> GetPrOverallSummary(string prYY)
        {
            return await GetPrOverallFromStoredProcedure(prYY);
        }

        public async Task<List<PrPendingDto>> GetPrOverallSummaryByDept(string prYY, string deptCode)
        {
            return await GetPrOverallFromStoredProcedure(prYY, deptCode);
        }

        public async Task<List<PrPendingDto>> GetPrPendingMain(string prYY)
        {
            return await GetPrPendingFromStoredProcedure(prYY, "Pur_SumPrPendingByPrMain");
        }

        public async Task<List<PrPendingDto>> GetPrPendingItem(string prYY)
        {
            return await GetPrPendingFromStoredProcedure(prYY, "Pur_SumPrPendingByPrItem");
        }

        private async Task<List<PrPendingDto>> GetPrPendingFromStoredProcedure(string prYY, string procedureName)
        {
            PrPendingModel = new List<PrPendingDto>();
            var conStr = configration.GetConnectionString("ConnectionSqlServer");
            using (var conn = new SqlConnection(conStr))
            {
                SqlDataReader dr;
                var sql = procedureName;
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prYY", prYY);
                if (conn.State == ConnectionState.Closed) { await conn.OpenAsync(); }
                dr = await cmd.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PrPendingModel.Add(new PrPendingDto
                        {
                            PrYear = dr["PrYear"].ToString(),
                            DeptCode = int.Parse(dr["DeptCode"].ToString()),
                            DeptSymbol = dr["DeptSymbol"].ToString(),
                            Division = dr["Division"].ToString(),
                            PrStatus = dr["PrStatus"].ToString(),
                            PrPending = int.Parse(dr["PrPending"].ToString()),
                        });
                    }
                    dr.Close();
                }
            }
            return PrPendingModel;
        }

        private async Task<List<PrPendingDto>> GetPrOverallFromStoredProcedure(string prYY, string deptCode = "")
        {
            PrPendingModel = new List<PrPendingDto>();
            var conStr = configration.GetConnectionString("ConnectionSqlServer");
            using (var conn = new SqlConnection(conStr))
            {
                SqlDataReader dr;
                var sql = "Pur_SumPrByPrStatus";
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prYY", prYY);
                cmd.Parameters.AddWithValue("@deptCode", deptCode);
                if (conn.State == ConnectionState.Closed) { await conn.OpenAsync(); }
                dr = await cmd.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PrPendingModel.Add(new PrPendingDto
                        {
                            PrYear = dr["PrYear"].ToString(),
                            DeptCode = 0,
                            DeptSymbol = "All",
                            Division = "All",
                            PrStatus = dr["PrStatus"].ToString(),
                            PrPending = int.Parse(dr["PrPending"].ToString()),
                        });
                    }
                    dr.Close();
                }
            }
            return PrPendingModel;
        }

        public async Task<List<NerDepartmentDto>> GetNerDepartment(string deptCode = "")
        {
            return (await dbContext.TbDepartments.Where(p => p.DeptCode.StartsWith(deptCode)).ToListAsync()).Select(NerDepartmentDto.FromTbDepartment).ToList();
        }

    }
}