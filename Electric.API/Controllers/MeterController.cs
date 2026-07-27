using Electric.API.Dtos.Common;
using Electric.API.Dtos.Meters;
using Electric.API.Services.Meters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Electric.API.Controllers
{
    [ApiController]
    [Route("api/meters")]
    public class MeterController : ControllerBase
    {
        private readonly IMeterService _meterService;

        /// <summary>
        /// Mapea la information del response en un StatusCode del ActionResult 
        /// </summary>
        /// <typeparam name="T">Ingrese el valor genérico que va a retornar</typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private ActionResult<ResponseDto<T>> ResponseStatus<T>(ResponseDto<T> response)
        {
            return StatusCode(response.StatusCode, new ResponseDto<T>
            {
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }

        public MeterController(IMeterService meterService)
        {
            _meterService = meterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<MeterDto>>> GetOneByIdAsync(string id)
        {
            var response = await _meterService.GetOneByIdAsync(id);

            return ResponseStatus(response);
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