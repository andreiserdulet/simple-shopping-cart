using System;

namespace Data.Abstraction
{
    public abstract class BaseEntityModel
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
