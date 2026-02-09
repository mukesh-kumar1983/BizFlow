namespace EmployeeService.Infrastructure.Storage;

/// <summary>
/// Local file system storage.
/// </summary>
public sealed class LocalFileStorage
{
    #region Fields

    private readonly string _basePath;

    #endregion

    #region Constructors

    public LocalFileStorage(string basePath)
    {
        _basePath = basePath;
        Directory.CreateDirectory(_basePath);
    }

    #endregion

    #region Public Methods

    public async Task<string> SaveAsync(string fileName, byte[] content)
    {
        var safeFileName = SanitizeFileName(fileName);
        var fullPath = Path.Combine(_basePath, safeFileName);

        await File.WriteAllBytesAsync(fullPath, content);
        return safeFileName;
    }

    #endregion

    #region Helpers

    private static string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();

        return new string(
            fileName
                .Replace(" ", "_")
                .Where(c => !invalidChars.Contains(c))
                .ToArray());
    }

    #endregion
}
