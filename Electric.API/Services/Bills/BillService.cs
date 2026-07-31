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

        public async Task<ResponseDto<PageDto<List<ShowBillDto>>>> GetPagesAsync(
            string MeterId = "",
            string ClientId = "",
            string searchTerm = "",
            int page = 1,
            int pageSize = 10)
        {
            page = Math.Abs(page); //Asegura que la pagina sea positiva (-2) -> 2
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            IQueryable<BillEntity> billQuery = _context.Bills;
            IQueryable<MeterEntity> meterQuery = _context.Meters;

            //Filtrando por Termino de Búsqueda
            /*if (!string.IsNullOrEmpty(searchTerm))
            {
                billQuery = billQuery
                    .Where(c => (c.)
                    .ToLower().Contains(searchTerm.ToLower()));
            }*/

            if (!string.IsNullOrEmpty(ClientId))
            {
                billQuery = billQuery.Where(b => 
                    meterQuery
                        .Where(m => m.ClientId == ClientId)
                        .Select(m => m.Id)
                        .Contains(b.MeterId)
                );
            }

            if (!string.IsNullOrEmpty(MeterId))
            {
                billQuery = billQuery.Where(b => b.MeterId == MeterId);
            }

            int totalRows = await billQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            page = page > totalPages ? 1 : page;

            int startIndex = (page - 1) * pageSize;

            var billsEntity = await billQuery
                .OrderByDescending(c => c.CurrentReadingDate)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var metersEntity = await meterQuery.ToListAsync();            

            return ResponseHelper.OK<PageDto<List<ShowBillDto>>>(
                totalRows > 0 ? "Registros Encontrados" : "Ninguna Coincidencia Encontrada",
                new PageDto<List<ShowBillDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = totalPages,
                    HasNextPage = page < totalPages,
                    HasPreviousPage = page > 1,
                    Items = BillMapper.ListEntityToListDto(billsEntity, metersEntity)
                });
        }

        public async Task<ResponseDto<ResponseBillDto>> EditAsync(string id, EditBillDto dto)
        {
            var billEntity = await _context.Bills.FirstOrDefaultAsync(b => b.Id == id);

            if(billEntity is null)
                return ResponseHelper.NotFound<ResponseBillDto>("Factura No Encontrada");

            if(billEntity.Paid == dto.Paid)
                return ResponseHelper.BadRequest<ResponseBillDto>("El Estado de Pagado no modifica el registro actual");

            var billEntityUpdate = BillMapper.EditDtoToEntity(dto,billEntity);

            _context.Update(billEntityUpdate);

            await _context.SaveChangesAsync();

            return ResponseHelper.OK<ResponseBillDto>("Factura Pagada Correctamente", new ResponseBillDto
            {
                Id = id
            });
        }

        public Task<ResponseDto<ResponseBillDto>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }


    }
}