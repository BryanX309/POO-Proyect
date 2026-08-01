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

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PageDto<List<ShowBillDto>>>>> GetPagesAsync(
            string MeterId = "",
            string ClientId = "",
            string searchTerm = "",
            int page = 1,
            int pageSize = 10)
        {
            var response = await _billService.GetPagesAsync
            (
            MeterId,
            ClientId,
            searchTerm,
            page,
            pageSize
            );

            return ResponseStatus(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ResponseBillDto>>> PayBill(string id,[FromBody] EditBillDto dto)
        {
            var response = await _billService.PaidAsync(id, dto);
            
            return ResponseStatus(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ResponseBillDto>>> Delete(string id)
        {
            var response = await _billService.DeleteAsync(id);
            
            return ResponseStatus(response);
        }
    }
}