using BiciReserva.Services;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BiciReserva.Controller.CReserva
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservaController :ControllerBase
    {
        private readonly IReservaService _reserva;
        private readonly ResponseService _response;
        public ReservaController(IReservaService reserva, ResponseService response)
        {
            this._reserva = reserva;
            this._response = response;
        }
        [HttpPost]
        public async Task<IActionResult> PostReserva([FromBody] ReservaDTO reserva)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var iduClaim = User.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.Name || c.Type == JwtRegisteredClaimNames.UniqueName);

                if (iduClaim != null && !string.IsNullOrEmpty(iduClaim.Value) && long.TryParse(iduClaim.Value, out long usuarioId))
                {
                    var result = await _reserva.PostReserva(reserva, usuarioId);
                    return _response.CreateResponse(result, result.code);
                }
            }

            return Unauthorized("Usuario no autorizado");
        }

        [HttpGet]
        public async Task<IActionResult> GetReservas()
        {
            var result = await _reserva.GetReservaList();
            return _response.CreateResponse(result, result.code);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetReservaById(long id)
        {
            var result = await _reserva.GetReservaById(id);
            return _response.CreateResponse(result, result.code);
        }

        [HttpPut]
        public async Task<IActionResult> PutReserva([FromBody] ReservaDTO reserva)
        {
            var result = await _reserva.PutReserva(reserva);
            return _response.CreateResponse(result, result.code);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteReserva(long id)
        {
            var result = await _reserva.DeleteReserva(id);
            return _response.CreateResponse(result, result.code);
        }
    }
}
