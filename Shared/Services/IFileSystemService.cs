using RTSADocs.Shared.DTOs;
using RTSADocs.Shared.Models;

namespace RTSADocs.Shared.Services
{
    public interface IFileSystemService
    {
        string FileSystemRootArchive { get; }
        string FileSystemRootMain { get; }

        Task<string> InitializeFileStore();
        Result MoveFileFromArchive(string filePath);
        Result MoveFileToArchive(string filePath);
        Task<Result<MemoryStream>> ReadFileFromFileStore(string filePath, FileSource source);
        Task<Result<string>> WriteFileToStore(MemoryStream memoryStream, string fileStore);
    }
}
