using RTSADocs.Shared.DTOs;
using RTSADocs.Shared.Models;

namespace RTSADocs.Shared.Services
{
    public interface IFileSystemService
    {
        string FileSystemRootArchive { get; }
        string FileSystemRootMain { get; }

        Result DecryptFile(string filePath, FileSource source);
        Result EncryptFile(string filePath, FileSource source);
        Task<string> InitializeFileStore();
        Result MoveFileFromArchive(string filePath);
        Result MoveFileToArchive(string filePath);
        Task<Result<MemoryStream>> ReadFileFromFileStore(string filePath, FileSource source);
        string ReadFileFromFileStoreAsBlob(string filePath, FileSource source);
        Task<Result<string>> WriteFileToStore(MemoryStream memoryStream, string fileStore);
    }
}
