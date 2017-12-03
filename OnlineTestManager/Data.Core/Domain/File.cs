using System;

namespace Data.Core.Domain
{
    public class File
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Url { get; set; }

        public Guid TestInstanceId { get; set; }
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
