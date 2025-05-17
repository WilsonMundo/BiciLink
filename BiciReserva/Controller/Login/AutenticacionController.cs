using BiciReserva.Services;
using BusinessLogic.DTO;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.Auth.DTO;
using Shared.DTO;

namespace BiciReserva.Controller.Login
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AutenticacionController : ControllerBase
    {
        private readonly ResponseService _response;
        private readonly IIAuthService _iIAuthService;
        public AutenticacionController(ResponseService response, IIAuthService iAuthService)
        {
            this._response = response;   
            this._iIAuthService = iAuthService;
        }

        [HttpGet("auth/verificar")]
        [AllowAnonymous]
        public ActionResult getAutenticacion()
        {
            if (User.Identity != null)
                if (User.Identity.IsAuthenticated)
                {
                    var userInfo = new UserInfoModel
                    {
                        Name = User.Identity.Name,
                        Claims = User.Claims.Select(c => new UserInfoModel.ClaimModel { Type = c.Type, Value = c.Value })

                    };

                    return Ok(userInfo);
                }
            return Ok(new UserInfoModel { });
        }
        [HttpPost("logout")]        
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLogin([FromBody] UserInfo userInfo)
        {
            try
            {
                ResultAPI<UserToken?> userToken = await _iIAuthService.ValidateUser(userInfo);
                if(userToken.result != null)
                {
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = userToken.result.Expiration,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    };

                    Response.Cookies.Append("AuthToken", userToken.result.Token, cookieOptions);
                    return Ok();

                }
                else
                {
                    return _response.CreateResponse(userToken, userToken.code);
                }

            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error al autenticar login");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno comunicar a soporte");
            }
        }

        [HttpPost("Auth/login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLoginRest([FromBody] UserInfo userInfo)
        {
            try
            {
                ResultAPI<UserToken?> userToken = await _iIAuthService.ValidateUser(userInfo);
                if ( userToken.result != null)
                {
                    return _response.CreateResponse(userToken, userToken.code);
                }
                else
                {
                    return _response.CreateResponse(userToken, userToken.code);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al autenticar login");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno comunicar a soporte");
            }
        }
        [HttpPost("register")]        
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegister user)
        {
            ResultAPI<object> result = await _iIAuthService.RegisterUser(user);
            return _response.CreateResponse(result, result.code);
        }

    }
}
