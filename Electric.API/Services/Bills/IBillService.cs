using Electric.API.Dtos.Bills;
using Electric.API.Dtos.Common;

namespace Electric.API.Services.Bills
{
    public interface IBillService
    {
        Task<ResponseDto<ResponseBillDto>> CreateAsync(CreateBillDto dto);
        Task<ResponseDto<ShowBillDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<PageDto<List<ShowBillDto>>>> GetPagesAsync(
            string searchTerm = "", //Búsquedas de un termino en cualquier campo
            int page = 1,
            int pageSize = 10);
        Task<ResponseDto<ResponseBillDto>> EditAsync(string id, EditBillDto dto);
        Task<ResponseDto<ResponseBillDto>> DeleteAsync(string id);
    }
}