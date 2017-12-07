﻿using System;

namespace Data.Core.Domain
{
    public class UserType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public static UserType Create(string type)
        {
            var instance = new UserType { Id = Guid.NewGuid() };
            instance.Update(type);
            return instance;
        }

        public void Update(string type)
        { 
            Type = type;
        }
    }
}