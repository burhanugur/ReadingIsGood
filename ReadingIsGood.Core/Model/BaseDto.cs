using System;

namespace ReadingIsGood.Core.Model
{
    public class BaseDto
    {
        #region Ctor
        public BaseDto()
        {
            CreateDate = DateTime.UtcNow;
        }
        #endregion

        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
