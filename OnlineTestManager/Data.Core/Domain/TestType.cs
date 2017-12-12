using System;

namespace Data.Core.Domain
{
    public class TestType : BaseEntity
    {
        
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
