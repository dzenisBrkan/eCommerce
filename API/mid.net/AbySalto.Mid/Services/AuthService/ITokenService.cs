using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.WebApi.Services.AuthService;

public interface ITokenService
{
    Task<string> GenerateJwtToken(User applicationUser);
}
