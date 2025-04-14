using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.WebApi.Services.FavoriteProductService;

namespace AbySalto.Mid.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favorite;

        public FavoriteController(IFavoriteService favorite)
        {
            _favorite = favorite;
        }

        [HttpPost("add-favorite/{id}")]
        [Authorize]
        public async Task<IActionResult> AddToFavorites(int id)
        {
            var applicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _favorite.AddToFavoritesAsync(int.Parse(applicationUserId), id);
            if (result)
            {
                return Ok("Product added to favorites!");
            }
            return Ok("Product removed from favorites!");
        }
    }
}
