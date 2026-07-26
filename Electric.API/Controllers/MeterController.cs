using Electric.API.Dtos.Common;
using Electric.API.Dtos.Meters;
using Electric.API.Services.Meters;
using Microsoft.AspNetCore.Mvc;

namespace Electric.API.Controllers
{
    [ApiController]
    [Route("api/meters")]
    public class MeterController:ControllerBase
    {
        private readonly IMeterService _meterService;

        public MeterController(IMeterService meterService)
        {
            _meterService = meterService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CreateMeterDto>>>> GetAll()
        {
            List<CreateMeterDto> lista = new List<CreateMeterDto>();

            for (int i = 1; i <= 5; i++)
            {
                lista.Add(new CreateMeterDto
                {
                   SupplyKey = (1111111*i).ToString(),
                   ClientId = Guid.NewGuid().ToString(),
                   ConsumptionType = $"Residencial {i}",
                   Rate = $"Tarifa {i}",
                   ComercialSector = "SRC"
                });
            }

            return new ResponseDto<List<CreateMeterDto>>
            {
                StatusCode = 400,
                Status = true,
                Message = "Generado",
                Data = lista
            };
        }

        [HttpPost]

        public async Task<ActionResult<ResponseDto<ResponseMeterDto>>>
        Post([FromBody] CreateMeterDto dto)
        {
            var response = await _meterService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new ResponseDto<ResponseMeterDto>
            {
               Status = response.Status,
               Message = response.Message,
               Data = response.Data 
            });
        }

        // [HttpPost]

        // public async Task<ActionResult<ResponseDto<ResponseCategoryDto>>>

        // Post([FromBody] CreateCategoryDto dto)
        // {
        //     var response = await _categoryService.CreateAsync(dto);

        //     return StatusCode(response.StatusCode, new ResponseDto<ResponseCategoryDto>
        //     {
        //        Status = response.Status,
        //        Message = response.Message,
        //        Data = response.Data
        //     });
        // }
    }
}