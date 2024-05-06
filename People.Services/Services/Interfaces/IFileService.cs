namespace People.Services.Services.Interfaces;

public interface IFileService
{
    string RootPath { get; }
    Task<byte[]> ReadBytesAsync(string path);
    Task WriteBytesAsync(string path, string content);
    Task DeleteAsync(string path);
    Task<string> ConvertToBase64Async(string path);
    Task<bool> DirectoryExistsAsync(string path);
    Task<bool> FileExistsAsync(string path);
}
