using BiciReserva.Services;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace BiciReserva.Controller.CMaintenance
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StateMaintenanceController:ControllerBase
    {
        private readonly ResponseService _response;
        private readonly IStateMaintenanceService _maintenance;
        public StateMaintenanceController(ResponseService response, IStateMaintenanceService maintenance)
        {
            this._response = response;
            this._maintenance = maintenance;

        }
        [HttpGet]
        public async Task<IActionResult> GetStateBicycle()
        {
            ResultAPI<List<GStateGeneralDTO>> result = await _maintenance.GetState();
            return _response.CreateResponse(result, result.code);
        }
        [HttpPost]
        public async Task<IActionResult> PostStateBicycle([FromBody] GEstadoDTO gState)
        {
            ResultAPI<GStateGeneralDTO> result = await _maintenance.PostState(gState);
            return _response.CreateResponse(result, result.code);
        }
        [HttpPut]
        public async Task<IActionResult> PutStateBicycle([FromBody] GStateGeneralDTO gState)
        {
            ResultAPI<GStateGeneralDTO> result = await _maintenance.PutState(gState);
            return _response.CreateResponse(result, result.code);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStateBicycle(short id)
        {
            ResultAPI<object> result = await _maintenance.DeleteState(id);
            return _response.CreateResponse(result, result.code);
        }

    }
}
