using Electric.API.Dtos.Bills;
using Electric.API.Entities;

namespace Electric.API.Mappers
{
    public class BillMapper
    {
        // public static BillEntity CreateNewBill(CreateBillDto dto) //oldBill puede ser nulo si dto es la primera factura a registrar
        // {
        //     BillEntity NewBill = ;

        //     return ;
        // }

        public static ShowBillDto BillInfoToDto(BillEntity bill, MeterEntity meter)
        {
            return new ShowBillDto()
            {
                Id = bill.Id,
                MeterInfo = MeterMapper.EntityToOneDto(meter),
                MeterId = bill.MeterId,
                ExpirationDate = bill.ExpirationDate.Date,
                PreviousReading = bill.PreviousReading,
                CurrentReading = bill.CurrentReading,
                PreviousReadingDate = bill.PreviousReadingDate.Date,
                CurrentReadingDate = bill.CurrentReadingDate.Date,
                Consumption = bill.CurrentReading - bill.PreviousReading,
                Paid = bill.Paid,
                TotalAmount = bill.TotalAmount
            };
        }

        public static List<ShowBillDto> ListEntityToListDto
        (List<BillEntity> bills, List<MeterEntity> meters)
        {
            List<ShowBillDto> BillsList = bills.Select(bill => new ShowBillDto
            {
                Id = bill.Id,
                MeterId = bill.MeterId,
                ExpirationDate = bill.ExpirationDate.Date,
                PreviousReading = bill.PreviousReading,
                CurrentReading = bill.CurrentReading,
                PreviousReadingDate = bill.PreviousReadingDate.Date,
                CurrentReadingDate = bill.CurrentReadingDate.Date,
                Consumption = bill.CurrentReading - bill.PreviousReading,
                Paid = bill.Paid,
                TotalAmount = bill.TotalAmount
            }).ToList();

            foreach (var bill in BillsList)
            {
                var MeterInfo = meters.FirstOrDefault(m => m.Id == bill.MeterId);

                if(MeterInfo is not null)
                    bill.MeterInfo = MeterMapper.EntityToOneDto(MeterInfo);
            }

            return BillsList;
        }

        public static BillEntity EditDtoToEntity(EditBillDto dto, BillEntity entity)
        {
            if(entity.Paid != dto.Paid)
            {
                entity.Paid = dto.Paid;
                entity.ModifiedById = "f51f7b25-0b93-46ce-be29-ca1db4762b77";
                entity.ModifiedDate = DateTime.Now.Date;
            }

            return entity;
        }
    }
}