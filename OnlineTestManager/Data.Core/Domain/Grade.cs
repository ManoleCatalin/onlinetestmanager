using System;

namespace Data.Core.Domain
{
    public class Grade 
    {
        public int Value { get; private set; }
        public DateTime MarkedAt { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; set; }
        public Guid TestInstanceId { get; private set; }
        public virtual TestInstance TestInstance { get; set; }
        public bool IsDeleted { get; set; }
        public static Grade Create(int value,Guid userId, Guid testInstanceId)
        {
            var instance = new Grade { MarkedAt = DateTime.Now, UserId = userId, TestInstanceId = testInstanceId };
            instance.Update(value,false);
            return instance;
        }

        public void Update(int value,bool isDeleted)
        {
            Value = value;
            IsDeleted = isDeleted;
        }

    }
}
