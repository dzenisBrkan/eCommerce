using AbySalto.Mid.Domain.Data;
using AbySalto.Mid.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.WebApi.Services.FavoriteProductService
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToFavoritesAsync(int applicationUserId, int productId)
        {
            var productExists = await _context.Favorites.FirstOrDefaultAsync(x=>x.ProductId == productId && x.UserId == applicationUserId);

            if (productExists == null)
            {
                var favorite = new Favorite
                {
                    UserId = applicationUserId,
                    ProductId = productId
                };

                _context.Favorites.Add(favorite);
            }
            else
            {
                _context.Favorites.Remove(productExists);
            }

            await _context.SaveChangesAsync();
            return productExists != null ? false : true;
        }
    }
}
