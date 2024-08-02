namespace MovieApi.Storage
{
    public interface IFileStorage
    {
        Task<string> Save(byte[] content, string extention, string container, string contentType);

        Task<string> Edit(byte[] content, string extention, string container, string contentType, string path);

        Task Delete(string container, string path);

    }
}
