using DMS.Shared.DTOs;
using DMS.Shared.Models;
using System.IO;
using System.Threading.Tasks;

namespace DMS.Shared.Services
{
    public interface IFileSystemService
    {
        string FileSystemRootArchive { get; } // Root directory for the archive file system
        string FileSystemRootMain { get; } // Root directory for the main file system

        Result DecryptFile(string filePath, FileSource source); // Decrypts a file at the specified path from the given source
        Result EncryptFile(string filePath, FileSource source); // Encrypts a file at the specified path from the given source
        Task<string> InitializeFileStore(); // Initializes the file store and returns the path
        Result MoveFileFromArchive(string filePath); // Moves a file from the archive to the main file system
        Result MoveFileToArchive(string filePath); // Moves a file from the main file system to the archive
        Task<Result<MemoryStream>> ReadFileFromFileStore(string filePath, FileSource source); // Reads a file from the file store and returns it as a memory stream
        string ReadFileFromFileStoreAsBlob(string filePath, FileSource source); // Reads a file from the file store and returns it as a blob
        byte[] ReadFileFromFileStoreAsBytes(string path, FileSource source); // Reads a file from the file store and returns it as a byte array
        Task<Result<string>> WriteFileToStore(MemoryStream memoryStream, string fileStore);// Writes a file to the file store from a memory stream and returns the path
        Task FileStoreCleanUp();      
    }
}

