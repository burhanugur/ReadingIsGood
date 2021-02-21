using System;

namespace ReadingIsGood.Domain
{
    public class BaseModel
    {
        #region Ctor
        public BaseModel()
        {
            CreateDate = DateTime.UtcNow;
        }
        #endregion

        public Guid Id { get; set; }

        public DateTime CreateDate { get; private set; }

        public DateTime? ModifyDate { get; set; }
    }
}
