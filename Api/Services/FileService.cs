using Api.Services.Interfaces;

namespace Api.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public FileService(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public string RootPath => Path.Combine(_hostingEnvironment.ContentRootPath, "Data");

    public async Task<byte[]> ReadBytesAsync(string path)
    {
        try
        {
            string filePath = Path.Combine(RootPath, path);
            if (!await FileExistsAsync(filePath)) return null;

            byte[] content = await File.ReadAllBytesAsync(filePath);
            return content;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task WriteBytesAsync(string path, string content)
    {
        try
        {
            string filePath = Path.Combine(RootPath, path);
            string directory = Path.GetDirectoryName(filePath);
            if (!await DirectoryExistsAsync(directory))
                Directory.CreateDirectory(directory);

            byte[] fileBytes = Convert.FromBase64String(content);
            await File.WriteAllBytesAsync(filePath, fileBytes);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task DeleteAsync(string path)
    {
        try
        {
            string filePath = Path.Combine(RootPath, path);
            if (!await FileExistsAsync(filePath)) return;

            await Task.Run(() => File.Delete(filePath));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> ConvertToBase64Async(string path)
    {
        try
        {
            string filePath = Path.Combine(RootPath, path);
            if (!await FileExistsAsync(filePath)) return string.Empty;

            byte[] fileBytes = await ReadBytesAsync(filePath);
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> DirectoryExistsAsync(string path)
    {
        try
        {
            return await Task.Run(() => Directory.Exists(path));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> FileExistsAsync(string path)
    {
        try
        {
            return await Task.Run(() => File.Exists(path));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
