using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBitesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> IsValidUser(LoginModel loginDetails)
        {
            var userDetailsList = await _loginService.IsValidUser(loginDetails);
            if (userDetailsList.Username == null)
            {
                return NotFound(userDetailsList);
            }
            return Ok(userDetailsList);
        }
    }
}
