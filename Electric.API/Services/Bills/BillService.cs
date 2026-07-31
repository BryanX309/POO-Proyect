using Electric.API.Database;
using Electric.API.Dtos.Bills;
using Electric.API.Dtos.Common;
using Electric.API.Entities;
using Electric.API.Helpers;
using Electric.API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Electric.API.Services.Bills
{
    public class BillService : IBillService
    {
        private readonly ElectricDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public BillService(ElectricDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        private string ValidateBillInfo(CreateBillDto dto, BillEntity oldBill)
        {
            if (oldBill.CurrentReadingDate == DateTime.Now)
            {
                return "ya hay una factura emitida del dia de hoy";
            }

            if (dto.CurrentReading <= oldBill.CurrentReading)
            {
                return "La Lectura Actual debe ser mayor que la Ultima Lectura";
            }

            return "";
        }

        public async Task<ResponseDto<ResponseBillDto>> CreateAsync(CreateBillDto dto)
        {
            int prevReading = 0;

            Console.WriteLine(dto);

            var meter = await _context.Meters.FirstOrDefaultAsync(c => c.Id == dto.MeterId);

            if (meter is null)
                return ResponseHelper.BadRequest<ResponseBillDto>($"El Id ingresado no corresponde a ningún Contador");

            //oldBill buscara la factura mas reciente de X Contador
            var oldBill = await _context.Bills
            .Where(c => c.MeterId == dto.MeterId)
            .OrderByDescending(c => c.CurrentReadingDate)
            .FirstOrDefaultAsync();

            if (oldBill is not null)
            {
                if (oldBill.CurrentReadingDate.Date == DateTime.Now.Date)
                    return ResponseHelper.BadRequest<ResponseBillDto>("Ya hay una factura emitida del dia de hoy");

                /*if (dto.CurrentReading <= oldBill.CurrentReading)
                    return ResponseHelper.BadRequest<ResponseBillDto>("La Lectura Actual debe ser mayor que la Ultima Lectura Registrada");
                    */
                prevReading = oldBill.CurrentReading;
            }

            BillEntity newBill = new BillEntity()
            {
                Id = Guid.NewGuid().ToString(),
                MeterId = dto.MeterId,

                CurrentReading = dto.CurrentReading,
                CurrentReadingDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(28),

                TotalAmount = (dto.CurrentReading - prevReading) * meter.Rate,

                Paid = false,

                CreatedById = "f51f7b25-0b93-46ce-be29-ca1db4762b77",
                CreatedDate = DateTime.Now
            };

            if (oldBill is not null)
            {
                newBill.PreviousReading = prevReading;
                newBill.PreviousReadingDate = oldBill.CurrentReadingDate.Date;
            }

            _context.Bills.Add(newBill);

            await _context.SaveChangesAsync();

            return ResponseHelper.OK<ResponseBillDto>("Factura Emitida Correctamente", new ResponseBillDto
            {
                Id = newBill.Id
            });
        }

        public async Task<ResponseDto<ShowBillDto>> GetOneByIdAsync(string id)
        {
            var Bill = await _context.Bills.FirstOrDefaultAsync(c => c.Id == id);

            if(Bill is null)
            {
                return ResponseHelper.NotFound<ShowBillDto>("Registro no Encontrado");
            }

            var MeterInfo = await _context.Meters.FirstOrDefaultAsync(c => c.Id == Bill.MeterId);

            if(MeterInfo is null)
            {
                return ResponseHelper.NotFound<ShowBillDto>("Medidor propietario no Encontrado");
            }

            return ResponseHelper.OK<ShowBillDto>("Factura Encontrada", BillMapper.BillInfoToDto(Bill, MeterInfo));
        }

        public Task<ResponseDto<PageDto<List<ShowBillDto>>>> GetPagesAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ResponseBillDto>> EditAsync(string id, EditBillDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ResponseBillDto>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }


    }
}