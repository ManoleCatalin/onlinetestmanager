using System;

namespace Data.Core.Domain
{
    public class TestType : IBaseEntity
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; }

        public static TestType Create(string type)
        {
            var instance = new TestType { Id = Guid.NewGuid() };
            instance.Update(type);
            return instance;
        }

        public void Update(string type)
        {
            Type = type;
        }
    }
}
