using System;

namespace Data.Core.Domain
{
    public class File
    {
        public Guid Id { get; private set; }
        public string Path { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Url { get; private set; }
        public Guid TestInstanceId { get; private set; }
        public virtual TestInstance TestInstance { get; set; }

        public static File Create(string path, string url, Guid testInstanceId)
        {
            var instance = new File { Id = Guid.NewGuid(), CreatedAt = DateTime.Now };
            instance.Update(path, url, testInstanceId);
            return instance;
        }

        public void Update(string path, string url, Guid testInstanceId)
        {
            Path = path;
            Url = url;
            TestInstanceId = testInstanceId;
        }
    }
}
