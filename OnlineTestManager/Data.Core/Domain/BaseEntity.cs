﻿using System;

namespace Data.Core.Domain
{
   public interface IBaseEntity
   {
       Guid Id { get; }
       bool IsDeleted { get;set; }
    }
}
