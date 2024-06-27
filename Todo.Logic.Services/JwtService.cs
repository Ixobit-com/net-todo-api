using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Todo.Common;
using Todo.Common.Constants;
using Todo.Common.Helpers;
using Todo.Common.Logging;
using Todo.Common.Settings;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class JwtService : BaseService, IJwtService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityManager _identityManager;
        private readonly Logger _logger;
        private readonly ClientHasherSettings _clientHasherSettings;
        private readonly JwtSettings _jwtSettings;

        public JwtService(
            IUnitOfWork unitOfWork,
            IIdentityManager identityManager,
            Logger logger,
            IOptions<ClientHasherSettings> clientHasherSettings,
            IOptions<JwtSettings> jwtSettings)
            : base() {
            _unitOfWork = unitOfWork;
            _identityManager = identityManager;
            _logger = logger;
            _clientHasherSettings = clientHasherSettings.Value;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ServiceResult<AccessTokenDto>> GrantClientCredentialsAsync(string clientKey, string clientSecret) {
            var client = GetActiveClientByCredentials(clientKey, clientSecret);
            if (client == null) {
                return ServiceResult<AccessTokenDto>.Failed(ResultCodes.Authorization.CLIENT_NOT_FOUND);
            }

            return await GenerateTokensAsync(client);
        }

        public async Task<ServiceResult<AccessTokenDto>> GrantResourceOwnerAsync(string clientKey, string clientSecret, string email, string password) {
            var client = GetActiveClientByCredentials(clientKey, clientSecret);
            if (client == null) {
                return ServiceResult<AccessTokenDto>.Failed(ResultCodes.Authorization.CLIENT_NOT_FOUND);
            }

            var user = _unitOfWork.GetRepository<User>()
                .Get(x => x.Email == email)
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .FirstOrDefault();

            if (user == null) {
                return ServiceResult<AccessTokenDto>.Failed(ResultCodes.Authorization.INCORRECT_USER_CREDENTIALS);
            }

            bool isPasswordCorrect = await _identityManager.CheckPasswordAsync(user, password);
            if (!isPasswordCorrect) {
                return ServiceResult<AccessTokenDto>.Failed(ResultCodes.Authorization.INCORRECT_USER_CREDENTIALS);
            }

            return await GenerateTokensAsync(client, user);
        }

        public async Task<ServiceResult<AccessTokenDto>> GrantRefreshTokenAsync(string clientKey, string clientSecret, string refreshToken) {
            var client = GetActiveClientByCredentials(clientKey, clientSecret);
            if (client == null) {
                return ServiceResult<AccessTokenDto>.Failed(ResultCodes.Authorization.CLIENT_NOT_FOUND);
            }

            var token = _unitOfWork.GetRepository<RefreshToken>()
                .Get(x => x.Token == refreshToken)
                .Include(x => x.User)
                    .ThenInclude(x => x.UserRoles)
                        .ThenInclude(x => x.Role)
                .FirstOrDefault();

            if (token == null || token.ClientId != client.Id || token.User != null) {
                return ServiceResult<AccessTokenDto>.Failed(ResultCodes.Authorization.INCORRECT_REFRESH_TOKEN);
            }

            return await GenerateTokensAsync(client, token.User);
        }

        private Client GetActiveClientByCredentials(string key, string secret) {
            var client = _unitOfWork.GetRepository<Client>()
                .Get(x => x.Key == key && x.IsActive)
                .Include(x => x.Scopes)
                    .ThenInclude(x => x.Scope)
                .FirstOrDefault();

            if (client == null) {
                return null;
            }

            var hash = ClientHelper.ComputeHash(secret, client.SecretSalt, _clientHasherSettings.Pepper, _clientHasherSettings.Iterations);
            if (hash != client.SecretHash) {
                return null;
            }

            return client;
        }

        private async Task<ServiceResult<AccessTokenDto>> GenerateTokensAsync(Client client, User? user = null) {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();

                var claims = new List<Claim> {
                    new Claim(TodoClaimTypes.ClientId, $"{client.Id}"),
                    new Claim(TodoClaimTypes.ClientName, client.Name)
                };

                if (client.Scopes != null) {
                    foreach (var scope in client.Scopes) {
                        claims.Add(new Claim(TodoClaimTypes.ClientScope, scope.Scope.Name));
                    }
                }

                if (user != null) {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

                    if (user.UserRoles != null) {
                        foreach (var userRole in user.UserRoles) {
                            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                        }
                    }
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

                DateTime expires = DateTime.UtcNow.AddSeconds(_jwtSettings.AcceessTokenLifeTime);

                var tokenDescription = new SecurityTokenDescriptor {
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    Subject = new ClaimsIdentity(claims),
                    Expires = expires,
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = tokenHandler.CreateToken(tokenDescription);

                string accessToken = tokenHandler.WriteToken(securityToken);
                string refreshToken = GenerateRefreshToken(client.Id, user?.Id);

                await _unitOfWork.CommitAsync();

                return ServiceResult<AccessTokenDto>.Successed(new AccessTokenDto {
                    AccessToken = accessToken,
                    ExpireIn = _jwtSettings.AcceessTokenLifeTime,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex) {
                _logger.Error(ex, "Generate tokens exception occured.");
                return ServiceResult<AccessTokenDto>.InternalServerError(ex.Message);
            }
        }

        private string GenerateRefreshToken(Guid clientId, Guid? userId) {
            using (var rng = RandomNumberGenerator.Create()) {
                var saltBytes = new byte[32];
                rng.GetBytes(saltBytes);

                var token = Convert.ToBase64String(saltBytes);

                _unitOfWork.GetRepository<RefreshToken>().Add(new RefreshToken {
                    ClientId = clientId,
                    UserId = userId,
                    Token = token,
                    GeneratedAt = DateTime.UtcNow
                });

                return token;
            }
        }
    }
}