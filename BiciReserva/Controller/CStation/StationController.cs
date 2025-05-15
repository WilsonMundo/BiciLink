using BiciReserva.Services;
using BusinessLogic.Interface;
using Domain.ModelContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace BiciReserva.Controller.CStation
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StationController: ControllerBase

    {
        private readonly ResponseService _response;
        private readonly IStationService _StationService;
        public StationController(ResponseService response, IStationService stationService)
        {
            this._response = response;
            this._StationService = stationService;
        }
        [HttpPost]
        public async Task<IActionResult> PostStation([FromBody] EstacionDTO estacion)
        {
            ResultAPI<EstacionDTO> result = await _StationService.InsertStation(estacion);
            return _response.CreateResponse(result, result.code);
        }
        [HttpGet]
        public async Task<IActionResult> GetStation()
        {
            ResultAPI<IEnumerable<EstacionDTO>> result = await _StationService.GetStation();
            return _response.CreateResponse(result, result.code);
        }
    }
}
