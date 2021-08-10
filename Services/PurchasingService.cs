using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ner_api.Data;
using ner_api.Entities;
using ner_pr_api.Interfaces;
using System.Globalization;
using ner_pr_api.Dtos.OutputDtos;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace ner_pr_api.Services
{
    public class PurchasingService : IPurchasingService
    {
        private readonly DatabaseContext db;

        private readonly CultureInfo th_TH = new CultureInfo("th-TH");

        private readonly string prYear = "PR";
        private readonly IConfiguration configration;

        public PurchasingService(DatabaseContext databaseContext, IConfiguration configration)
        {
            this.configration = configration;
            this.db = databaseContext;
            prYear += DateTime.Now.ToString("yy", th_TH);
        }

        public async Task<List<TbPrMain>> GetPurchasingByDate(DateTime dFrom, DateTime dTo)
        {
            return await (db.TbPrMains.Where(p => p.DateNow >= dFrom.Date && p.DateNow <= dTo.Date).Include(p => p.TbPrMainDetails.Where(p => p.ItemsStatus == true))).ToListAsync();
        }

        public async Task<List<TbPrMain>> GetPurchasingByDocNo(string PrNo)
        {
            return await (db.TbPrMains.Where(p => p.PrNo == PrNo).Include(p => p.TbPrMainDetails.Where(p => p.ItemsStatus == true))).ToListAsync();
        }

        public async Task<List<PurchasingDto>> GetPurchasingByDeptCode(string prYear, string deptCode, string status = "checked")
        {
            var result = (await (db.TbPrMains.Where(p => p.PrNo.StartsWith(prYear) && p.DeptCode == deptCode && p.Status == status).Include(p => p.TbPrMainDetails.Where(p => p.ItemsStatus == true)).Include(p => p.User)).ToListAsync()).Select(PurchasingDto.FromPrMain).ToList();
            return result;
        }

        public async Task<List<PurchasingDescDto>> GetPurchasingDescByDocNo(string PrNo)
        {
            return (await db.TbPrMainDetails.Where(p => p.PrNo == PrNo && p.ItemsStatus == true).OrderBy(p => p.Item).ToListAsync()).Select(PurchasingDescDto.fromTbPrMainDetail).ToList();
        }

        public async Task<List<PurchasingDto>> GetPurchasingMainByStatus(string status = "checked")
        {
            var result = (await (db.TbPrMains.Where(p => p.PrNo.StartsWith(prYear) && p.Status == status).Include(p => p.TbPrMainDetails.Where(p => p.ItemsStatus == true)).Include(p => p.User)).ToListAsync()).Select(PurchasingDto.FromPrMain).ToList();
            return result;
        }

        public async Task<List<TbPrMain>> GetPurchasingByStatus(string prNo = "", string status = "checked")
        {
            return await (db.TbPrMains.Where(p => p.PrNo.StartsWith(prNo) && p.Status == status).Include(p => p.TbPrMainDetails.Where(p => p.ItemsStatus == true))).ToListAsync();
        }

        public async Task<int> UpdatePrStatus(string prNo, string oldStatus, string newStatus, string operationName)
        {
            var _lookingPr = await db.TbPrMains.Where(p => p.PrNo == prNo && p.Status == oldStatus).FirstOrDefaultAsync();
            if (_lookingPr == null)
            {
                throw new Exception("สถานะใบขอซื้อไม่ถูกต้อง !");
            }

            _lookingPr.Status = newStatus;
            _lookingPr.OperateDate = DateTime.Now.ToString("dd-MMM-yyyy", th_TH);
            _lookingPr.OperatelName = operationName;

            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdatePrItemStatus(int item, string poNo = "", string poDate = "", bool isAppr = false)
        {
            var _lookingPr = await db.TbPrMainDetails.Where(p => p.Item == item && p.ApproveStatus == false).FirstOrDefaultAsync();
            if (_lookingPr == null)
            {
                throw new Exception("สถานะใบขอซื้อไม่ถูกต้อง !");
            }

            if (!isAppr)
            {
                poNo = "***ไม่อนุมัติสั่งซื้อ***";
            }

            DateTime _poDate = DateTime.Now;
            DateTime.TryParse(poDate, out _poDate);
            _lookingPr.Po = poNo;
            _lookingPr.ApproveStatus = isAppr;
            _lookingPr.Podate = _poDate;
            return await db.SaveChangesAsync();
        }

        public async Task<int> AcknowledgePr(string prNo = "", string name = "")
        {
            var _lookingPr = await db.TbPrMains.Where(p => p.PrNo == prNo && p.IsAckn == false).FirstOrDefaultAsync();
            if (_lookingPr == null)
            {
                throw new Exception("สถานะใบขอซื้อไม่ถูกต้อง !");
            }

            _lookingPr.AcknDate = DateTime.Now;
            _lookingPr.IsAckn = true;
            _lookingPr.AcknName = name;
            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateTimeLimit(DateTime dFrom, DateTime dTo)
        {
            var _lookingPr = await db.TbPrTimeSettings.Where(p => p.Id == 1).FirstOrDefaultAsync();
            if (_lookingPr == null)
            {
                throw new Exception("สถานะใบขอซื้อไม่ถูกต้อง !");
            }

            _lookingPr.InitTime = dFrom;
            _lookingPr.EndTime = dTo;
            return await db.SaveChangesAsync();
        }

        public async Task<TimeSettingDto> GetTimePeriodSetting()
        {
            var result = await db.TbPrTimeSettings.Where(p => p.Id == 1).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("ไม่พบข้อมูลการตั้งค่า");
            }

            return TimeSettingDto.FromTbSetting(result);
        }

        public async Task<List<PurchasingDescDto>> GetPurchasingItemByStatus(string status = "checked")
        {
            var PrPendingModel = new List<PurchasingDescDto>();
            var conStr = configration.GetConnectionString("ConnectionSqlServer");
            using (var conn = new SqlConnection(conStr))
            {
                SqlDataReader dr;
                var sql = "Pur_GetPrMainDetails";
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prStatus", status);
                if (conn.State == ConnectionState.Closed) { await conn.OpenAsync(); }
                dr = await cmd.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var _poDate = dr["POdate"].ToString();
                        if (_poDate != "")
                        {
                            var _splitDate = DateTime.Parse(_poDate).ToString("dd/MM/yyyy", th_TH);
                            _poDate = _splitDate;
                        }
                        
                        PrPendingModel.Add(new PurchasingDescDto
                        {
                            PrNo = dr["PrNo"].ToString(),
                            PrStatus = dr["PrStatus"].ToString(),
                            PrType = dr["PrType"].ToString(),
                            DeptCode = dr["DeptCode"].ToString(),
                            DeptSymbol = dr["DeptSymbol"].ToString(),
                            Division = dr["Division"].ToString(),
                            FullName = dr["FullName"].ToString(),
                            DateCreated = dr["DateCreated"].ToString(),
                            TimeCreated = dr["TimeCreated"].ToString(),
                            Remark = dr["Remark"].ToString(),
                            ApproveDate = dr["ApproveDate"].ToString(),
                            ApproveTime = dr["ApproveTime"].ToString(),
                            ApproveName = dr["ApproveName"].ToString(),
                            BudgetsStatus = dr["BudgetsStatus"].ToString(),
                            Item = 0,
                            PdCode = dr["PdCode"].ToString(),
                            PdDetail = dr["PdDetail"].ToString(),
                            Qty = dr["Qty"].ToString(),
                            Keeping = dr["Keeping"].ToString(),
                            BdgCode = dr["BdgCode"].ToString(),
                            Objective = dr["Objective"].ToString(),
                            Po = dr["Po"].ToString(),
                            PoDate = _poDate,
                            ApproveStatus = bool.Parse(dr["ApproveStatus"].ToString()),
                            ItemsStatus = true,
                        });
                    }
                    dr.Close();
                }
            }
            return PrPendingModel;
        }

        public async Task<List<PurchasingDescDto>> GetPurchasingItemByDate(string dFrom, string dTo)
        {
            DateTime _dFrom = DateTime.Now.Date;
            DateTime _dTo = DateTime.Now.Date;

            DateTime.TryParse(dFrom, out _dFrom);
            DateTime.TryParse(dTo, out _dTo);

            var PrPendingModel = new List<PurchasingDescDto>();
            var conStr = configration.GetConnectionString("ConnectionSqlServer");
            using (var conn = new SqlConnection(conStr))
            {
                SqlDataReader dr;
                var sql = "Pur_GetPrMainDetailsByDate";
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dFrom", dFrom);
                cmd.Parameters.AddWithValue("@dTo", dTo);
                // cmd.Parameters.Add("@dFrom", SqlDbType.DateTime).Value = _dFrom;
                // cmd.Parameters.Add("@dTo", SqlDbType.DateTime).Value = _dTo;
                if (conn.State == ConnectionState.Closed) { await conn.OpenAsync(); }
                dr = await cmd.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var _poDate = dr["POdate"].ToString();
                        if (_poDate != "")
                        {
                            var _splitDate = DateTime.Parse(_poDate).ToString("dd/MM/yyyy", th_TH);
                            _poDate = _splitDate;
                        }

                        PrPendingModel.Add(new PurchasingDescDto
                        {
                            PrNo = dr["PrNo"].ToString(),
                            PrType = dr["PrType"].ToString(),
                            PrStatus = dr["PrStatus"].ToString(),
                            DeptCode = dr["DeptCode"].ToString(),
                            DeptSymbol = dr["DeptSymbol"].ToString(),
                            Division = dr["Division"].ToString(),
                            FullName = dr["FullName"].ToString(),
                            DateCreated = dr["DateCreated"].ToString(),
                            TimeCreated = dr["TimeCreated"].ToString(),
                            Remark = dr["Remark"].ToString(),
                            ApproveDate = dr["ApproveDate"].ToString(),
                            ApproveTime = dr["ApproveTime"].ToString(),
                            ApproveName = dr["ApproveName"].ToString(),
                            BudgetsStatus = dr["BudgetsStatus"].ToString(),
                            Item = 0,
                            PdCode = dr["PdCode"].ToString(),
                            PdDetail = dr["PdDetail"].ToString(),
                            Qty = dr["Qty"].ToString(),
                            Keeping = dr["Keeping"].ToString(),
                            BdgCode = dr["BdgCode"].ToString(),
                            Objective = dr["Objective"].ToString(),
                            Po = dr["Po"].ToString(),
                            PoDate = _poDate,
                            ApproveStatus = bool.Parse(dr["ApproveStatus"].ToString()),
                            ItemsStatus = true,
                        });
                    }
                    dr.Close();
                }
            }
            return PrPendingModel;
        }

    }
}