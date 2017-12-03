using System;

namespace Data.Core.Domain
{
    public class Grade
    {
        public int Value { get; set; }
        public DateTime MarkedAt { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid TestInstanceId { get; set; }
        public virtual TestInstance TestInstance { get; set; }


        public static Grade Create(int value,Guid userId, Guid testInstanceId)
        {
            var instance = new Grade { MarkedAt = DateTime.Now, UserId = userId, TestInstanceId = testInstanceId };
            instance.Update(value);
            return instance;
        }

        public void Update(int value)
        {
            Value = value;  
        }

    }
}
