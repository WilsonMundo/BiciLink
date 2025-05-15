using BiciReserva.Services;
using BusinessLogic.Services.SBicicleta;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace BiciReserva.Controller.CBicycle
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StateBicycleController:ControllerBase
    {
        private readonly ResponseService _response;
        private readonly StateBicycleService _stateBicycle;
        public StateBicycleController(ResponseService response , StateBicycleService stateBicycle)
        {
            this._response = response;
            this._stateBicycle = stateBicycle;
        }
        [HttpGet]
        public async Task<IActionResult> GetStateBicycle()
        {
            ResultAPI<List<GStateGeneralDTO>> result = await _stateBicycle.GetState();
            return _response.CreateResponse(result, result.code);
        }
        [HttpPost]
        public async Task<IActionResult> PostStateBicycle([FromBody] GEstadoDTO gState )
        {
            ResultAPI<GStateGeneralDTO> result = await _stateBicycle.PostState(gState);
            return _response.CreateResponse(result, result.code);
        }
        [HttpPut]
        public async Task<IActionResult> PutStateBicycle([FromBody] GStateGeneralDTO gState)
        {
            ResultAPI<GStateGeneralDTO> result = await _stateBicycle.PutState(gState);
            return _response.CreateResponse(result, result.code);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStateBicycle(short id)
        {
            ResultAPI<object> result = await _stateBicycle.DeleteState(id);
            return _response.CreateResponse(result, result.code);
        }
    }
}
