using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Constants;
using Todo.API.Controllers.Base;
using Todo.API.Filters;
using Todo.API.Models;
using Todo.Common;
using Todo.Common.Constants;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models;

namespace Todo.API.Controllers {
    public class OAuthController : BaseController {
        private readonly IJwtService _jwtService;

        public OAuthController(
            IJwtService jwtService) {
            _jwtService = jwtService;
        }

        /// <summary>
        /// Gets access token to make authorized API calls
        /// </summary>
        /// <param name="data">Token credentials</param>
        /// <returns>Access token details</returns>
        [HttpPost("token")]
        [AllowAnonymous]
        [ValidateActionFilter<AuthModel>]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseResultModel<AccessTokenModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        public async Task<IActionResult> Token([FromBody] AuthModel data) {
            ServiceResult<AccessTokenDto>? result = null;

            switch (data.GrantType) {
                case AccessTokenGrantTypes.ClientCredentials:
                    result = await _jwtService.GrantClientCredentialsAsync(data.ClientKey, data.ClientSecret);
                    break;
                case AccessTokenGrantTypes.ResourceOwner:
                    result = await _jwtService.GrantResourceOwnerAsync(data.ClientKey, data.ClientSecret, data.Email, data.Password);
                    break;
                case AccessTokenGrantTypes.RefreshToken:
                    result = await _jwtService.GrantRefreshTokenAsync(data.ClientKey, data.ClientSecret, data.RefreshToken);
                    break;
                default:
                    result = ServiceResult<AccessTokenDto>.Failed(ResultCodes.Authorization.UNKNOWN_GRANT_TYPE, data.GrantType);
                    break;
            }

            return GetResponseResult(result);
        }
    }
}