using System;

namespace Data.Core.Domain
{
    public class TestType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }


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
