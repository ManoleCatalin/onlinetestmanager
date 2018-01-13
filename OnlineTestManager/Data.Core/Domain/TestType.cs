using System;

namespace Data.Core.Domain
{
    public class TestType : IBaseEntity
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public bool IsDeleted { get; set; }
        public static TestType Create(string type)
        {
            var instance = new TestType { Id = Guid.NewGuid() };
            instance.Update(type,false);
            return instance;
        }

        public void Update(string type, bool isDeleted)
        {
            Type = type;
            IsDeleted = isDeleted;
        }
    }
}
