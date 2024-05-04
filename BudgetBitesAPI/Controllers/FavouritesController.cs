using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBitesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        public readonly IFavouriteService _favouriteService;

        public FavouritesController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        [HttpPost]
        public async Task<IActionResult> UserFavouriteFood(FavouriteModel favouriteDetails)
        {
            var favouritesList = await _favouriteService.UserFavouriteFood(favouriteDetails);
            if (favouritesList.ToList().Count == 0)
            {
                return NotFound(favouritesList);
            }
            return Ok(favouritesList);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserFavourites(FavouriteModel favouriteDetails)
        {
            var favouritesList = await _favouriteService.SaveUserFavouriteFood(favouriteDetails);
            if(favouritesList.ToList().Count == 0)
            {
                return NotFound(favouritesList);
            }
            return Ok(favouritesList);
        }
    }
}
