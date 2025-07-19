using BiciReserva.Services;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BiciReserva.Controller.CMaintenance
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MaintenanceController:ControllerBase
    {
        private readonly IMaintenanceService _maintenance;
        private readonly ResponseService _response;
        public MaintenanceController(IMaintenanceService maintenance,ResponseService response)
        {
            this._maintenance = maintenance;
            this._response = response;   
        }

        [HttpPost]
        public async Task<IActionResult> PostMaintenance([FromBody] MantenimientoDTO mantenimiento)
        {           
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var iduClaim = User.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.Name || c.Type == JwtRegisteredClaimNames.UniqueName);

                if (iduClaim != null && !string.IsNullOrEmpty(iduClaim.Value) && long.TryParse(iduClaim.Value, out long tecnicoId))
                {
                    var result = await _maintenance.PostMaintenance(mantenimiento, tecnicoId);
                    return _response.CreateResponse(result, result.code);
                }
            }

            return Unauthorized("Usuario no autorizado");
        }

        [HttpGet]
        public async Task<IActionResult> GetMantenimientos()
        {
            var result = await _maintenance.GetMaintenanceList();
            return _response.CreateResponse(result, result.code);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetMaintenanceById(long id)
        {
            var result = await _maintenance.GetMaintenanceById(id);
            return _response.CreateResponse(result, result.code);
        }

        [HttpPut]
        public async Task<IActionResult> PutMaintenance([FromBody] MantenimientoDTO mantenimiento)
        {
            var result = await _maintenance.PutMaintenance(mantenimiento);
            return _response.CreateResponse(result, result.code);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteMaintenance(long id)
        {
            var result = await _maintenance.DeleteMaintenance(id);
            return _response.CreateResponse(result, result.code);
        }
    }
}
