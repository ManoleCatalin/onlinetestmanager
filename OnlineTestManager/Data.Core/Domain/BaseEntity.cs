using System;

namespace Data.Core.Domain
{
   public abstract class BaseEntity
    {
        public Guid Id { get;  protected set; }
    }
}
