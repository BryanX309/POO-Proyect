using Electric.API.Dtos.Common;
using Electric.API.Dtos.Meters;
using Electric.API.Entities;

namespace Electric.API.Services.Meters
{
    public interface IMeterService
    {
        Task<ResponseDto<ResponseMeterDto>> CreateAsync(CreateMeterDto dto);
        Task<ResponseDto<MeterDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<PageDto<List<MeterDto>>>> GetPagesAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<ResponseMeterDto>> EditAsync(string id, EditMeterDto dto);
        Task<ResponseDto<ResponseMeterDto>> DeleteAsync(string id);
    }
}