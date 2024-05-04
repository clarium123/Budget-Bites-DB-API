using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using BudgetBitesAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBitesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        public readonly IUserDetailsService _userService;
        public UserDetailsController(IUserDetailsService userService) {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel userDetails)
        {
            var isUserCreated = await _userService.CreateUser(userDetails);

            if (isUserCreated == null)
            {
                return BadRequest();
            }
            return Ok(isUserCreated);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoginUsers()
        {
            var userDetailsList = await _userService.GetAllLoginUsers();
            if (userDetailsList == null)
            {
                return NotFound();
            }
            return Ok(userDetailsList);
        }
    }
}
