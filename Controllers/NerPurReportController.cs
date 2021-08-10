using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ner_pr_api.Dtos.OutputDtos;
using ner_pr_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ner_pr_api.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("AllowNerFrontend")]
    [Route("api/[Controller]")]
    public class NerPurReportController : ControllerBase
    {
        private CultureInfo th_TH = new CultureInfo("th-TH");
        private string defaultPrYY = "";
        private readonly IPurchasingReportService IPUR;
        public NerPurReportController(IPurchasingReportService purService)
        {
            this.IPUR = purService;
            defaultPrYY = "PR" + DateTime.Now.ToString("yy", th_TH);
        }

        [Route("GetPrOverallSummary")]
        [HttpGet]
        public async Task<ActionResult> GetPrOverallSummary(string prYY)
        {
            if (prYY == null || prYY == "")
            {
                prYY = defaultPrYY;
            }
            try
            {
                var data = await IPUR.GetPrOverallSummary(prYY);
                return Ok(new { model = data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPrOverallSummaryByDept")]
        [HttpGet]
        public async Task<ActionResult> GetPrOverallSummaryByDept(string prYY, string deptCd)
        {
            if (deptCd == null || deptCd == "")
            {
                return BadRequest("กรุณาระบุรหัสแผนก");
            }

            if (prYY == null || prYY == "")
            {
                prYY = defaultPrYY;
            }
            try
            {
                var data = await IPUR.GetPrOverallSummaryByDept(prYY, deptCd);
                return Ok(new { model = data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPrPendingMain")]
        [HttpGet]
        public async Task<ActionResult> GetPrPendingMain(string prYY)
        {
            if (prYY == null || prYY == "")
            {
                prYY = defaultPrYY;
            }
            try
            {
                List<PrPendingDto> data = await IPUR.GetPrPendingMain(prYY);
                data = DataShapingDepartment(data);
                return Ok(new { model = data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPrPendingMainByMonth")]
        [HttpGet]
        public async Task<ActionResult> GetPrPendingMainByMonth(string prYY)
        {
            if (prYY == null || prYY == "")
            {
                prYY = defaultPrYY;
            }
            try
            {
                List<PrPendingDto> data = await IPUR.GetPrPendingMain(prYY);
                data = DataShapingMonth(data);
                return Ok(new { model = data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPrPendingMainByMonthDept")]
        [HttpGet]
        public async Task<ActionResult> GetPrPendingMainByMonthDept(string prYY)
        {
            if (prYY == null || prYY == "")
            {
                prYY = defaultPrYY;
            }
            try
            {
                List<PrPendingDto> data = await IPUR.GetPrPendingMain(prYY);
                data = DataShapingMonthDepartment(data);
                return Ok(new { model = data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDepartment")]
        public async Task<ActionResult> GetDepartment(string deptCode = "")
        {
            try
            {
                List<NerDepartmentDto> data = await IPUR.GetNerDepartment(deptCode);
                return Ok(new { model = data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPrPendingPrItem")]
        [HttpGet]
        public async Task<ActionResult> GetPrPendingPrItem(string prYY)
        {
            if (prYY == null || prYY == "")
            {
                prYY = defaultPrYY;
            }
            try
            {
                List<PrPendingDto> data = await IPUR.GetPrPendingItem(prYY);
                PrPendingDto rtnData = DataShapingStatus(data);
                return Ok(new { model = rtnData });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private List<PrPendingDto> DataShapingDepartment(List<PrPendingDto> model)
        {
            var returnData = new List<PrPendingDto>();
            var _dataShaping = (from c in model
                                group c by c.DeptCode into cg
                                select new
                                {
                                    cg.FirstOrDefault().DeptCode,
                                    cg.FirstOrDefault().DeptSymbol,
                                    cg.FirstOrDefault().Division,
                                    cg.FirstOrDefault().PrStatus,
                                    //prPending = cg.Sum(p => p.PrPending),
                                }).ToList();
            foreach (var item in _dataShaping)
            {
                var _prPending = model.Where(p => p.DeptCode == item.DeptCode && p.PrStatus == "pending").Sum(p => p.PrPending);
                var _prCanceled = model.Where(p => p.DeptCode == item.DeptCode && p.PrStatus == "canceled").Sum(p => p.PrPending);
                var _prChecked = model.Where(p => p.DeptCode == item.DeptCode && p.PrStatus == "checked").Sum(p => p.PrPending);
                var _prApproved = model.Where(p => p.DeptCode == item.DeptCode && p.PrStatus == "approved").Sum(p => p.PrPending);
                var _prCompleted = model.Where(p => p.DeptCode == item.DeptCode && p.PrStatus == "completed").Sum(p => p.PrPending);
                int _all = _prChecked + _prApproved + _prCompleted;
                double _kpi = Math.Round(((double)_prChecked / (double)_all) * 100, 2);
                returnData.Add(new PrPendingDto
                {
                    PrYear = "All",
                    DeptCode = item.DeptCode,
                    DeptSymbol = item.DeptSymbol,
                    Division = item.Division,
                    PrStatus = item.PrStatus,
                    PrPending = _prPending,
                    PrCanceled = _prCanceled,
                    PrChecked = _prChecked,
                    PrApproved = _prApproved,
                    PrCompleted = _prCompleted,
                    PrAll = _all,
                    PrKpi = _kpi <= 0 ? 100 + _kpi : 100 - _kpi
                });
            }
            var _avgApi = (double)(returnData.Sum(p => p.PrChecked) / (double)returnData.Sum(p => p.PrAll)) * 100;
            _avgApi = _avgApi <= 0 ? 100 : 100 - _avgApi;
            returnData.ForEach(p => p.AvgKpi = Math.Round(_avgApi, 2));
            return returnData.OrderBy(p => p.DeptCode).ToList();
        }

        private List<PrPendingDto> DataShapingMonthDepartment(List<PrPendingDto> model)
        {
            var returnData = new List<PrPendingDto>();
            var _dataShaping = (from c in model
                                group c by new { c.PrYear, c.DeptCode } into cg
                                select new
                                {
                                    cg.FirstOrDefault().PrYear,
                                    cg.FirstOrDefault().DeptCode,
                                    cg.FirstOrDefault().DeptSymbol,
                                    cg.FirstOrDefault().Division,
                                    cg.FirstOrDefault().PrStatus,
                                    //prPending = cg.Sum(p => p.PrPending),
                                }).ToList();
            foreach (var item in _dataShaping)
            {
                var _prPending = model.Where(p => p.PrYear == item.PrYear && p.DeptCode == item.DeptCode && p.PrStatus == "pending").Sum(p => p.PrPending);
                var _prCanceled = model.Where(p => p.PrYear == item.PrYear && p.DeptCode == item.DeptCode && p.PrStatus == "canceled").Sum(p => p.PrPending);
                var _prChecked = model.Where(p => p.PrYear == item.PrYear && p.DeptCode == item.DeptCode && p.PrStatus == "checked").Sum(p => p.PrPending);
                var _prApproved = model.Where(p => p.PrYear == item.PrYear && p.DeptCode == item.DeptCode && p.PrStatus == "approved").Sum(p => p.PrPending);
                var _prCompleted = model.Where(p => p.PrYear == item.PrYear && p.DeptCode == item.DeptCode && p.PrStatus == "completed").Sum(p => p.PrPending);
                int _all = _prChecked + _prApproved + _prCompleted;
                double _kpi = Math.Round(((double)_prChecked / (double)_all) * 100, 2);

                var _prYY = item.PrYear;
                var _splitYY = _prYY.Split('/');
                if (_splitYY.Length == 2)
                {
                    _prYY = _splitYY[0] + ' ' + ToThaiMonth(_splitYY[1]);
                }

                returnData.Add(new PrPendingDto
                {
                    PrYear = _prYY,
                    DeptCode = item.DeptCode,
                    DeptSymbol = item.DeptSymbol,
                    Division = item.Division,
                    PrStatus = item.PrStatus,
                    PrPending = _prPending,
                    PrCanceled = _prCanceled,
                    PrChecked = _prChecked,
                    PrApproved = _prApproved,
                    PrCompleted = _prCompleted,
                    PrAll = _all,
                    PrKpi = _kpi <= 0 ? 100 + _kpi : 100 - _kpi
                });
            }
            var _avgApi = (double)(returnData.Sum(p => p.PrChecked) / (double)returnData.Sum(p => p.PrAll)) * 100;
            _avgApi = _avgApi <= 0 ? 100 : 100 - _avgApi;
            returnData.ForEach(p => p.AvgKpi = Math.Round(_avgApi, 2));
            return returnData.OrderBy(p => p.DeptCode).ToList();
        }

        private List<PrPendingDto> DataShapingMonth(List<PrPendingDto> model)
        {
            var returnData = new List<PrPendingDto>();
            var _dataShaping = (from c in model
                                group c by new { c.PrYear } into cg
                                select new
                                {
                                    cg.FirstOrDefault().PrYear,
                                }).ToList();
            int yy = 0;
            string mm = "";
            foreach (var item in _dataShaping)
            {
                var _findYY = item.PrYear.Split('/');
                if (_findYY.Length == 2)
                {
                    int.TryParse(_findYY[0], out yy);
                    yy = yy + 543;
                    mm = _findYY[1];
                }

                var _prPending = model.Where(p => p.PrYear == item.PrYear && p.PrStatus == "pending").Sum(p => p.PrPending);
                var _prCanceled = model.Where(p => p.PrYear == item.PrYear && p.PrStatus == "canceled").Sum(p => p.PrPending);
                var _prChecked = model.Where(p => p.PrYear == item.PrYear && p.PrStatus == "checked").Sum(p => p.PrPending);
                var _prApproved = model.Where(p => p.PrYear == item.PrYear && p.PrStatus == "approved").Sum(p => p.PrPending);
                var _prCompleted = model.Where(p => p.PrYear == item.PrYear && p.PrStatus == "completed").Sum(p => p.PrPending);
                int _all = _prChecked + _prApproved + _prCompleted;
                double _kpi = Math.Round(((double)_prChecked / (double)_all) * 100, 2);

                returnData.Add(new PrPendingDto
                {
                    PrYear = $"{yy}/{mm}",
                    DeptCode = 0,
                    DeptSymbol = "All",
                    Division = "All",
                    PrStatus = "All",
                    PrPending = _prPending,
                    PrCanceled = _prCanceled,
                    PrChecked = _prChecked,
                    PrApproved = _prApproved,
                    PrCompleted = _prCompleted,
                    PrAll = _all,
                    PrKpi = _kpi <= 0 ? 100 + _kpi : 100 - _kpi
                });
            }

            mm = "";
            for (int i = 1; i <= 12; i++)
            {
                mm = $"{i}";
                if (i < 10)
                {
                    mm = $"0{i}";
                }

                var _lookingMM = returnData.Where(p => p.PrYear.EndsWith($"/{mm}")).FirstOrDefault();
                if (_lookingMM != null) { continue; }
                returnData.Add(new PrPendingDto
                {
                    PrYear = $"{yy}/{mm}",
                    DeptCode = 0,
                    DeptSymbol = "All",
                    Division = "All",
                    PrStatus = "checked",
                    PrPending = 0,
                });
            }
            var _avgApi = (double)(returnData.Sum(p => p.PrChecked) / (double)returnData.Sum(p => p.PrAll)) * 100;
            _avgApi = _avgApi <= 0 ? 100 : 100 - _avgApi;
            returnData.ForEach(p => p.AvgKpi = Math.Round(_avgApi, 2));
            return returnData.OrderBy(p => p.PrYear).ToList();
        }

        private PrPendingDto DataShapingStatus(List<PrPendingDto> model)
        {
            var returnData = new PrPendingDto();

            var _prPending = model.Where(p => p.PrStatus == "pending").Sum(p => p.PrPending);
            var _prCanceled = model.Where(p => p.PrStatus == "canceled").Sum(p => p.PrPending);
            var _prChecked = model.Where(p => p.PrStatus == "checked").Sum(p => p.PrPending);
            var _prApproved = model.Where(p => p.PrStatus == "approved").Sum(p => p.PrPending);
            var _prCompleted = model.Where(p => p.PrStatus == "completed").Sum(p => p.PrPending);

            returnData = new PrPendingDto
            {
                PrYear = "All",
                DeptCode = 0,
                DeptSymbol = "All",
                Division = "All",
                PrStatus = "All",
                PrPending = _prPending,
                PrCanceled = _prCanceled,
                PrChecked = _prChecked,
                PrApproved = _prApproved,
                PrCompleted = _prCompleted,
                PrAll = _prChecked + _prApproved + _prCompleted,
                PrKpi = 0
            };
            return returnData;
        }

        private string ToThaiMonth(string _prYY)
        {
            var _month = "";
            switch (_prYY)
            {
                case "01":
                    {
                        _month = "(01)มกราคม";
                        break;
                    }
                case "02":
                    {
                        _month = "(02)กุมภาพันธ์";
                        break;
                    }
                case "03":
                    {
                        _month = "(03)มีนาคม";
                        break;
                    }
                case "04":
                    {
                        _month = "(04)เมษายน";
                        break;
                    }
                case "05":
                    {
                        _month = "(05)พฤษภาคม";
                        break;
                    }
                case "06":
                    {
                        _month = "(06)มิถุนายน";
                        break;
                    }
                case "07":
                    {
                        _month = "(07)กรกฎาคม";
                        break;
                    }
                case "08":
                    {
                        _month = "(08)สิงหาคม";
                        break;
                    }
                case "09":
                    {
                        _month = "(09)กันยายน";
                        break;
                    }
                case "10":
                    {
                        _month = "(10)ตุลาคม";
                        break;
                    }
                case "11":
                    {
                        _month = "(11)พฤศจิกายน";
                        break;
                    }
                case "12":
                    {
                        _month = "(12)ธันวาคม";
                        break;
                    }
            }
            return _month;
        }

    }
}