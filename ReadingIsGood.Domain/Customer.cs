using System;
using System.Collections;
using System.Collections.Generic;

namespace ReadingIsGood.Domain
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
