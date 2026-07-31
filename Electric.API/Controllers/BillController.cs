using Electric.API.Controllers.Helpers;
using Electric.API.Dtos.Bills;
using Electric.API.Dtos.Common;
using Electric.API.Services.Bills;
using Microsoft.AspNetCore.Mvc;

namespace Electric.API.Controllers
{
    [ApiController]
    [Route("api/bills")]
    public class BillController : CustomBaseController
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ResponseBillDto>>> 
        Post([FromBody] CreateBillDto dto)
        {
            var response = await _billService.CreateAsync(dto);

            return ResponseStatus(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ShowBillDto>>>
        GetOneById(string id)
        {
            var response = await _billService.GetOneByIdAsync(id);

            return ResponseStatus(response);
        }
    }
}