
namespace MovieApi.Storage
{
    public class LocalStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LocalStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }


        public  Task Delete(string container, string path)
        {
            if (path != null)
            {
                var fileName = Path.GetFileName(path);
                string folder = Path.Combine(_env.WebRootPath, container, fileName);
                if (File.Exists(folder))
                {
                    File.Delete(folder);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> Edit(byte[] content, string extention, string container, string contentType, string path)
        {
            await Delete(container, path);
            return await Save(content, extention, container, contentType);
        }

        public async Task<string> Save(byte[] content, string extention, string container, string contentType)
        {
            var fileName = $"{Guid.NewGuid()}{extention}";
            string folder = Path.Combine(_env.WebRootPath, container);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string path = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(path, content);

            var url = $"{_httpContextAccessor?.HttpContext?.Request.Scheme}://{_httpContextAccessor?.HttpContext?.Request.Host}";
            var finalUrl = Path.Combine(url, container, fileName).Replace("\\", "/");

            return finalUrl;

        }
    }
}
