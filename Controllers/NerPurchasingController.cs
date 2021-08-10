using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ner_pr_api.Interfaces;
using System.Globalization;
using ner_pr_api.Dtos.InputDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;

namespace ner_pr_api.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("AllowNerFrontend")]
    [Route("api/[Controller]")]
    public class NerPurchasingController : ControllerBase
    {
        private CultureInfo th_TH = new CultureInfo("th-TH");
        private readonly IPurchasingService IPUR;
        private string defaultPrYY = "";
        private readonly IaccountService accountService;


        public NerPurchasingController(IPurchasingService Ipur, IaccountService account)
        {
            this.IPUR = Ipur;
            this.accountService = account;
            defaultPrYY = "PR" + DateTime.Now.ToString("yy", th_TH);
        }

        [Route("GetPurchasingByStatus")]
        [HttpGet]
        public async Task<ActionResult> GetPurchasingByStatus(string prNo = "", string status = "checked")
        {
            if (prNo == null || prNo == "")
            {
                prNo = defaultPrYY;
            }
            try
            {
                var result = await IPUR.GetPurchasingByStatus(prNo, status);
                return Ok(new { prmodel = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPurchasingMainByStatus")]
        [HttpGet]
        public async Task<ActionResult> GetPurchasingMainByStatus(string status = "checked")
        {
            try
            {
                var result = await IPUR.GetPurchasingMainByStatus(status);
                return Ok(new { prmodel = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPurchasingDescByStatus")]
        [HttpGet]
        public async Task<ActionResult> GetPurchasingDescByStatus(string status = "checked")
        {
            try
            {
                var result = await IPUR.GetPurchasingItemByStatus(status);
                return Ok(new { prmodel = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPurchasingDescByDate")]
        [HttpGet]
        public async Task<ActionResult> GetPurchasingDescByDate(string dFrom, string dTo)
        {
            if (dFrom == null || dFrom == "" || dTo == null || dTo == "")
            {
                return BadRequest("กรุณาระบุเลขที่่ PR");
            }

            // var _dFrom = dFrom;
            // var _dTo = dTo;
            // var _df = dFrom.Split('-');
            // if (_df.Length == 3)
            // {
            //     _dFrom = (int.Parse(_df[0]) + 543).ToString() + "-" + _df[1] + "-" + _df[2];
            // }

            // var _dt = dTo.Split('-');
            // if (_df.Length == 3)
            // {
            //     _dTo = (int.Parse(_dt[0]) + 543).ToString() + "-" + _dt[1] + "-" + _dt[2];
            // }

            try
            {
                var result = await IPUR.GetPurchasingItemByDate(dFrom, dTo);
                return Ok(new { prmodel = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPurchasingByDocNo")]
        [HttpGet]
        public async Task<ActionResult> GetPurchasingByDocNo(string prNo)
        {
            if (prNo == null || prNo == "")
            {
                return BadRequest("กรุณาระบุเลขที่่ PR");
            }

            try
            {
                var result = await IPUR.GetPurchasingByDocNo(prNo);
                return Ok(new { prmodel = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPurchasingByDeptCode")]
        [HttpGet]
        public async Task<ActionResult> GetPurchasingByDeptCode(string prNo, string deptCode)
        {
            if (prNo == null || prNo == "")
            {
                prNo = defaultPrYY;
            }

            if (deptCode == null || deptCode == "")
            {
                return BadRequest("กรุณาระบุรหัสแผนก");
            }

            try
            {
                var result = await IPUR.GetPurchasingByDeptCode(prNo, deptCode);
                return Ok(new { prmodel = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPurchasingDescByDocNo")]
        [HttpGet]
        public async Task<ActionResult> GetPurchasingDescByDocNo(string prNo)
        {
            if (prNo == null || prNo == "")
            {
                return BadRequest("กรุณาระบุเลขที่่ PR");
            }

            try
            {
                var result = await IPUR.GetPurchasingDescByDocNo(prNo);
                return Ok(new { prmodel = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPeriodTimeSetting")]
        [HttpGet]
        public async Task<ActionResult> GetPeriodTimeSetting()
        {
            try
            {
                var result = await IPUR.GetTimePeriodSetting();
                return Ok(new { model = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("AcknowledgePr")]
        [HttpPut]
        public async Task<ActionResult> AcknowledgePr(string prNo)
        {
            var _account = await GetUserInfo();
            if (_account == null)
            {
                return Unauthorized();
            }

            if (prNo == null || prNo == "")
            {
                return BadRequest("กรุณาระบุเลขที่่ PR");
            }
            try
            {
                int result = await IPUR.AcknowledgePr(prNo, _account.FullName);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CancelPurchaseRequest")]
        [HttpPut]
        public async Task<ActionResult> CancelPurchaseRequest(CancelPrDto model)
        {
            var _account = await GetUserInfo();
            if (_account == null)
            {
                return Unauthorized();
            }

            if (model.prNo == null || model.prNo == "") { return BadRequest("กรุณาระบุเลขที่่ PR"); }
            try
            {
                int result = await IPUR.UpdatePrStatus(model.prNo, model.oldStatus, model.newStatus, _account.FullName);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdatePrItemStatus")]
        [HttpPut]
        public async Task<ActionResult> UpdatePrItemStatus(int item, string poNo = "", string poDate = "", bool isAppr = false)
        {
            if (item == 0) { return BadRequest("กรุณาระบุเลขที่่ PR"); }

            try
            {
                if (poDate == null || poDate == "")
                {
                    poDate = DateTime.Now.ToString("yyyy-MM-dd");
                }

                int result = await IPUR.UpdatePrItemStatus(item, poNo, poDate, isAppr);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdateTimeLimit")]
        [HttpPut]
        public async Task<ActionResult> UpdateTimeLimit(string dFrom, string dTo)
        {
            if (dFrom == null || dTo == null)
            {
                return BadRequest();
            }

            var _dFrom = DateTime.Now.ToString("yyyy-MM-dd ") + dFrom + ":00";
            var _dTo = DateTime.Now.ToString("yyyy-MM-dd ") + dTo + ":00";

            try
            {
                int result = await IPUR.UpdateTimeLimit(DateTime.Parse(_dFrom), DateTime.Parse(_dTo));
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Account> GetUserInfo()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (accessToken == null)
            {
                return null;
            }

            return accountService.GetInfo(accessToken);
        }

    }
}