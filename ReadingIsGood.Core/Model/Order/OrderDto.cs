using ReadingIsGood.Core.Model.Stock;
using ReadingIsGood.Domain.DomainEnums;
using System;

namespace ReadingIsGood.Core.Model.Order
{
    public class OrderDto : BaseDto
    {
        public StockStatus Status { get; set; }

        public Guid CustomerId { get; set; }

        public Guid StockId { get; set; }

        public CustomerDto Customer { get; set; }

        public StockDto Stock { get; set; }
    }
}
