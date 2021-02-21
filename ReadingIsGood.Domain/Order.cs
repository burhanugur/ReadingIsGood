using ReadingIsGood.Domain.DomainEnums;
using System;

namespace ReadingIsGood.Domain
{
    public class Order : BaseModel
    {
        public StockStatus Status { get; set; }

        public Guid CustomerId { get; set; }

        public Guid StockId { get; set; }

        public Customer Customer { get; set; }

        public Stock Stock { get; set; }
    }
}
