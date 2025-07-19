using BiciReserva.Services;
using BusinessLogic.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace BiciReserva.Controller.CReserva
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EstadoReservaController:ControllerBase
    {
        private readonly IStateReservaService _stateReserva;
        private readonly ResponseService _response;

        public EstadoReservaController(IStateReservaService stateReserva,ResponseService response)
        {
            this._stateReserva = stateReserva;
            this._response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetStateReserva()
        {
            var result = await _stateReserva.GetState();
            return _response.CreateResponse(result, result.code);
        }

        [HttpPost]
        public async Task<IActionResult> PostStateReserva([FromBody] GEstadoDTO gState)
        {
            var result = await _stateReserva.PostState(gState);
            return _response.CreateResponse(result, result.code);
        }

        [HttpPut]
        public async Task<IActionResult> PutStateReserva([FromBody] GStateGeneralDTO gState)
        {
            var result = await _stateReserva.PutState(gState);
            return _response.CreateResponse(result, result.code);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStateReserva(short id)
        {
            var result = await _stateReserva.DeleteState(id);
            return _response.CreateResponse(result, result.code);
        }
    }
}
