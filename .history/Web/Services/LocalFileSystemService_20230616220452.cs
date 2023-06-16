using Humanizer;
using Microsoft.VisualBasic;

using RTSADocs.Data;
using RTSADocs.Data.Services;
using RTSADocs.Shared.DTOs;
using RTSADocs.Shared.Models;
using RTSADocs.Shared.Services;
using static MudBlazor.Colors;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Contracts;

using System.IO;

using System.Security.Policy;

namespace RTSADocs.Services
{
    internal class LocalFileSystemService : IFileSystemService
    {
        // Root directory for the main file store
        public string FileSystemRootMain => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Main";

        // Root directory for the archive file store
        public string FileSystemRootArchive => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Archive";

        public LocalFileSystemService()
        {
            Init();
        }

        // Initialize the file system by creating the main and archive directories if they don't exist
        private void Init()
        {
            if (!Directory.Exists(FileSystemRootMain))
            {
                Directory.CreateDirectory(FileSystemRootMain);
            }
            if (!Directory.Exists(FileSystemRootArchive))
            {
                Directory.CreateDirectory(FileSystemRootArchive);
            }
        }

        // Initialize the file store by generating a random folder structure
        public Task<string> InitializeFileStore()
        {
            var folderName = GenerateRandomStructure();
            return Task.FromResult(folderName);
        }

        // Generate a random folder structure with a specified maximum depth
        private string GenerateRandomStructure(int maxDepth = 6)
        {
            var depth = new Random().Next(2, maxDepth);
            var finalStructure = "";
            for (int i = 0; i < depth; i++)
            {
                finalStructure += $"\\{Guid.NewGuid().ToString()[..8]}";
            }
            return finalStructure;
        }

        // Write a file to the file store
        public async Task<Result<string>> WriteFileToStore(MemoryStream memoryStream, string fileStore)
        {
            try
            {
                var randomFolder = GenerateRandomStructure(4);
                var fileName = Guid.NewGuid().ToString() + ".dms";
                var path = Path.Combine(fileStore, randomFolder);
                var filePath = Path.Combine(FileSystemRootMain + path);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                else { }
                var fullPath = Path.Combine(filePath, fileName);
                FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write);

                await Task.Run(() =>
                {
                    memoryStream.WriteTo(fileStream);
                    fileStream.Close();
                });

                return Result<string>.Success(Path.Combine(path, fileName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return Result<string>.Failure("An error occurred while trying to write a file to the file store.");
            }
        }

        // Move a file from the main file store to the archive file store
        public Result MoveFileToArchive(string filePath)
        {
            try
            {
                var currentPath = Path.Combine(FileSystemRootMain + filePath);
                var newPath = Path.Combine(FileSystemRootArchive + filePath);
                var dir = Path.GetDirectoryName(newPath);
                if (!Directory.Exists(dir) && !string.IsNullOrEmpty(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                else { }
                File.Move(currentPath, newPath);
                return Result.Success();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return Result.Failure("An error occurred while trying to move the file to the archive.");
            }
        }

                // Move a file from the archive file store back to the main file store
        public Result MoveFileFromArchive(string filePath)
        {
            try
            {
                var currentPath = Path.Combine(FileSystemRootArchive + filePath);
                var newPath = Path.Combine(FileSystemRootMain + filePath);
                File.Move(currentPath, newPath);
                return Result.Success();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return Result.Failure("An error occurred while trying to move the file from the archive.");
            }
        }

        // Read a file from the file store as a MemoryStream
        public async Task<Result<MemoryStream>> ReadFileFromFileStore(string filePath, FileSource source)
        {
            var memoryStream = new MemoryStream();
            var finalPath = source switch
            {
                FileSource.main => Path.Combine(FileSystemRootMain, filePath),
                FileSource.archive => Path.Combine(FileSystemRootArchive, filePath),
                _ => throw new InvalidOperationException("Unknown file store source")
            };
            if (File.Exists(finalPath))
            {
                var fileStream = File.OpenRead(finalPath);
                await fileStream.CopyToAsync(memoryStream);
                return Result<MemoryStream>.Success(memoryStream);
            }
            return Result<MemoryStream>.Failure("File not found.");
        }

        // Read a file from the file store as a Base64-encoded string representing a data URL (blob)
        public string ReadFileFromFileStoreAsBlob(string path, FileSource source)
        {
            var finalPath = source switch
            {
                FileSource.main => Path.Combine(FileSystemRootMain + path),
                FileSource.archive => Path.Combine(FileSystemRootArchive + path),
                _ => throw new InvalidOperationException("Unknown file store source")
            };

            if (File.Exists(finalPath))
            {
                try
                {
                    var fileBytes = File.ReadAllBytes(finalPath);
                    var base64String = Convert.ToBase64String(fileBytes);
                    var mimeType = "application/pdf";
                    var dataUrl = $"data:{mimeType};base64,{base64String}";
                    return dataUrl;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            throw new InvalidOperationException("File not found in the source.");
        }

        // Read a file from the file store as a byte array
        public byte[] ReadFileFromFileStoreAsBytes(string path, FileSource source)
        {
            var finalPath = source switch
            {
                FileSource.main => Path.Combine(FileSystemRootMain + path),
                FileSource.archive => Path.Combine(FileSystemRootArchive + path),
                _ => throw new InvalidOperationException("Unknown file store source")
            };

            if (File.Exists(finalPath))
            {
                var fileBytes = File.ReadAllBytes(finalPath);
                return fileBytes;
            }

            throw new InvalidOperationException("File not found in the source.");
        }

        // Decrypt a file from the file store
        public Result DecryptFile(string filePath, FileSource source)
        {
            try
            {
                // Add code to decrypt the file here
                return Result.Success("File decrypted.");
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
                return Result.Failure("File decryption failed.");
            }
        }

        // Encrypt a file from the file store
        public Result EncryptFile(string filePath, FileSource source)
        {
            try
            {
                // Add code to encrypt the file here
                return Result.Success("File encrypted.");
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
                return Result.Failure("File encryption failed.");
            }
        }
    }
}
