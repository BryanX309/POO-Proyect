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
    }
}