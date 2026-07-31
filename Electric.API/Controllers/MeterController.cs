using Electric.API.Controllers.Helpers;
using Electric.API.Dtos.Common;
using Electric.API.Dtos.Meters;
using Electric.API.Services.Meters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Electric.API.Controllers
{
    [ApiController]
    [Route("api/meters")]
    public class MeterController : CustomBaseController
    {
        private readonly IMeterService _meterService;

        public MeterController(IMeterService meterService)
        {
            _meterService = meterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<MeterDto>>> GetOneByIdAsync(string id)
        {
            var response = await _meterService.GetOneByIdAsync(id);

            return ResponseStatus<MeterDto>(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PageDto<List<MeterDto>>>>> GetPagesAsync
        (
            string supplyKey = "", //Búsquedas directas de la Clave de Suministro 
            string clientId = "", //Búsquedas de Contadores del Mismo Cliente
            string comercialSector = "", //Búsquedas dentro del mismo Sector Comercial
            string searchTerm = "", //Búsquedas de un termino en cualquier campo
            int page = 1,
            int pageSize = 10)
        {
            var response = await _meterService.GetPagesAsync(supplyKey, clientId, comercialSector, searchTerm, page, pageSize);

            return ResponseStatus(response);
        }


        [HttpPost]

        public async Task<ActionResult<ResponseDto<ResponseMeterDto>>>
        Post([FromBody] CreateMeterDto dto)
        {
            var response = await _meterService.CreateAsync(dto);

            return ResponseStatus(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ResponseMeterDto>>>
        Edit(string id, [FromBody] EditMeterDto dto)
        {
            var response = await _meterService.EditAsync(id, dto);

            return ResponseStatus(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ResponseMeterDto>>>
        Delete(string id)
        {
            var response = await _meterService.DeleteAsync(id);

            return ResponseStatus(response);
        }
    }
}