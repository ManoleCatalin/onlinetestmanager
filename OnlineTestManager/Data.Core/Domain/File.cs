using System;

namespace Data.Core.Domain
{
    public class File : IBaseEntity
    {
        public Guid Id { get; private set; }
        public string Path { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Url { get; private set; }
        public Guid TestInstanceId { get; private set; }
        public virtual TestInstance TestInstance { get; set; }
        public bool IsDeleted { get; set; }
        public static File Create(string path, string url, Guid testInstanceId)
        {
            var instance = new File { Id = Guid.NewGuid(), CreatedAt = DateTime.Now };
            instance.Update(path, url, testInstanceId,false);
            return instance;
        }

        public void Update(string path, string url, Guid testInstanceId,bool isDeleted)
        {
            Path = path;
            Url = url;
            TestInstanceId = testInstanceId;
            IsDeleted = isDeleted;
        }
    }
}
