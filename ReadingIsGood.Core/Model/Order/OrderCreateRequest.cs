using System;

namespace ReadingIsGood.Core.Model.Order
{
    public class OrderCreateRequest
    {
        public Guid CustomerId { get; set; }

        public Guid StockId { get; set; }
    }
}
