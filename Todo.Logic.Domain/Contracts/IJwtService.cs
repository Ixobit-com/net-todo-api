using Todo.Common;
using Todo.Logic.Domain.Models;

namespace Todo.Logic.Domain.Contracts {
    public interface IJwtService {
        Task<ServiceResult<AccessTokenDto>> GrantClientCredentialsAsync(string clientKey, string clientSecret);
        Task<ServiceResult<AccessTokenDto>> GrantResourceOwnerAsync(string clientKey, string clientSecret, string email, string password);
        Task<ServiceResult<AccessTokenDto>> GrantRefreshTokenAsync(string clientKey, string clientSecret, string refreshToken);
    }
}