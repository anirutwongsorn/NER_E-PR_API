using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ner_pr_api.Dtos.InputDtos;
using ner_pr_api.Dtos.OutputDtos;
using ner_pr_api.Interfaces;

namespace ner_pr_api.Controllers
{
    [ApiController]
    [EnableCors("AllowNerFrontend")]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IaccountService accountService;

        public AccountController(IaccountService account)
        {
            this.accountService = account;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register(RegisterRequest registerRequest)
        {
            await accountService.Register(registerRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var account = await accountService.Login(loginRequest.Username, loginRequest.Password);
            if (account == null)
            {
                return Unauthorized();
            }
            var model = new MemberDto
            {
                FullName = account.FullName,
                Token = accountService.GenerateToken(account),
            };
            return Ok(new { account = model });
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> Info()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (accessToken == null)
            {
                return Unauthorized();
            }

            var account = accountService.GetInfo(accessToken);
            return Ok(account);
        }

    }
}