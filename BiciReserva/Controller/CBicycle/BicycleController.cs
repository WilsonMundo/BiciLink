using BiciReserva.Services;
using BusinessLogic.Interface;
using BusinessLogic.Services.User;
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
    public class BicycleController :ControllerBase
    {
        private readonly ResponseService _response;
        private readonly IBicycleService _bicycleService;

        public BicycleController(ResponseService response, IBicycleService bicycleService)
        {
            this._response = response;
            this._bicycleService = bicycleService;
            
        }

        [HttpPost]
        public async Task<IActionResult> PostBicycle([FromBody] BicycleDTO bicycle)
        {
            ResultAPI<BicycleDTO> result = await _bicycleService.PostBicycle(bicycle);
            return _response.CreateResponse(result, result.code);
        }
        [HttpGet]
        public async Task<IActionResult> GetBicycle()
        {
            ResultAPI<List<BicycleDTO>> result = await _bicycleService.GetBicycles();
            return _response.CreateResponse(result, result.code);
        }
    }
}
