using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator, IConfiguration configuration, IMapper mapper ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator; 
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;

        [HttpPost("Create-User")]
        public async Task<ActionResult<UserInfoViewModel>>CreateUser(CreateUserCommand command)
        {
            var request = await _mediator.Send(command);

            if(request.ResponseInfo is null)
            {
                var userinfo = request.Value;
                if (userinfo is not null)
                {
                    var cookieOptionsToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    _ = int.TryParse(_configuration["JWT:RefreshTokenExpirationTimeInDays"], out int refreshTokenExpirationTimeInDays);

                    var cookieOptionsRefreshToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(refreshTokenExpirationTimeInDays)
                    };

                    Response.Cookies.Append("jwt", request.Value!.TokenJWT!, cookieOptionsToken);
                    Response.Cookies.Append("refreshToken", request.Value.RefreshToken!, cookieOptionsRefreshToken);

                    return Ok(_mapper.Map<UserInfoViewModel>(request.Value));

                }
            }
            return BadRequest(request);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<ResponseBase<UserInfoViewModel>>> Login(LoginUserCommand command)
        {
            var request = await _mediator.Send(command);

            if (request.ResponseInfo is null)
            {
                var userinfo = request.Value;
                if (userinfo is not null)
                {
                    var cookieOptionsToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    _ = int.TryParse(_configuration["JWT:RefreshTokenExpirationTimeInDays"], out int refreshTokenExpirationTimeInDays);

                    var cookieOptionsRefreshToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(refreshTokenExpirationTimeInDays)
                    };

                    Response.Cookies.Append("jwt", request.Value!.TokenJWT!, cookieOptionsToken);
                    Response.Cookies.Append("refreshToken", request.Value.RefreshToken!, cookieOptionsRefreshToken);

                    return Ok(_mapper.Map<UserInfoViewModel>(request.Value));

                }
            }
            return BadRequest(request);
        }


        [HttpPost("RefreshToken")]
        public async Task<ActionResult<ResponseBase<UserInfoViewModel>>> RefreshToken(RefreshTokenCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid request.");
            }

            var request = await _mediator.Send(new RefreshTokenCommand
            {
                Username = command.Username,
                RefreshToken = Request.Cookies["refreshToken"]
            });

            if (request != null)
            {
                return Ok(request); 
            }

            
            return BadRequest("Failed to refresh token.");
        }



    }
}
