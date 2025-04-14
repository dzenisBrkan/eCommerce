namespace AbySalto.Mid.WebApi.Services.FavoriteProductService
{
    public interface IFavoriteService
    {
        Task<bool> AddToFavoritesAsync(int applicationUserId, int productId);
    }
}