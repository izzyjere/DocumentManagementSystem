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
        public string FileSystemRootMain => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Main";
        public string FileSystemRootArchive => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Archive";

        public LocalFileSystemService()
        {
            Init();
        }
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
        public Task<string> InitializeFileStore()
        {
            var folderName = GenerateRandomStructure();
            return Task.FromResult(folderName);
        }
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

                await Task.Run(() => memoryStream.WriteTo(fileStream));
                return Result<string>.Success(Path.Combine(path, fileName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+e.StackTrace);
                return Result<string>.Failure("An error occured while trying to write file to filestore.");
            }

        }
        public Result MoveFileToArchive(string filePath)
        {
            try
            {
                var currentPath = Path.Combine(FileSystemRootMain + filePath);
                var newPath = Path.Combine(FileSystemRootArchive+ filePath);
                var dir = Path.GetDirectoryName(newPath);
                if (!Directory.Exists(dir)&& !string.IsNullOrEmpty(dir))
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
                return Result.Failure("An error occured while trying to move file to archive.");
            }
        }
        public Result MoveFileFromArchive(string filePath)
        {
            try
            {
                var currentPath = Path.Combine(FileSystemRootArchive, filePath);
                var newPath = Path.Combine(FileSystemRootMain, filePath);
                File.Move(currentPath, newPath);
                return Result.Success();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return Result.Failure("An error occured while trying to move file from archive.");
            }
        }
        public async Task<Result<MemoryStream>> ReadFileFromFileStore(string filePath, FileSource source)
        {
            var memoryStream = new MemoryStream();
            var finalPath = source switch
            {
                FileSource.main => Path.Combine(FileSystemRootMain, filePath),
                FileSource.archive => Path.Combine(FileSystemRootArchive, filePath),
                _ => throw new InvalidOperationException("Unknown filestore source")
            };
            if (File.Exists(finalPath))
            {
                var fileStream = File.OpenRead(finalPath);
                await fileStream.CopyToAsync(memoryStream);
                return Result<MemoryStream>.Success(memoryStream);
            }
            return Result<MemoryStream>.Failure("File not found.");
        }
        public string ReadFileFromFileStoreAsBlob(string path, FileSource source)
        {
            var finalPath = source switch
            {
                FileSource.main => Path.Combine(FileSystemRootMain+ path),
                FileSource.archive => Path.Combine(FileSystemRootArchive+ path),
                _ => throw new InvalidOperationException("Unknown filestore source")
            };

            if (File.Exists(finalPath))
            {
                var fileBytes = File.ReadAllBytes(finalPath);
                var base64String = Convert.ToBase64String(fileBytes);
                var mimeType = "application/pdf";
                var dataUrl = $"data:{mimeType};base64,{base64String}";
                return dataUrl;
            }

            throw new InvalidOperationException("File not found in  source");
        }    
        public byte[] ReadFileFromFileStoreAsBytes(string path, FileSource source)
        {
            var finalPath = source switch
            {
                FileSource.main => Path.Combine(FileSystemRootMain+ path),
                FileSource.archive => Path.Combine(FileSystemRootArchive+ path),
                _ => throw new InvalidOperationException("Unknown filestore source")
            };

            if (File.Exists(finalPath))
            {
                var fileBytes = File.ReadAllBytes(finalPath);                
                return fileBytes;
            }

            throw new InvalidOperationException("File not found in  source");
        }      
        public Result DecryptFile(string filePath, FileSource source)
        {
            try
            {

                return Result.Success("File Decrypted.");
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
                return Result.Failure("File Decryption failed.");
            }

        }
        public Result EncryptFile(string filePath, FileSource source)
        {
            try
            {
                return Result.Success("File Decrypted.");
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
                return Result.Failure("File Decryption failed.");
            }
        }
    }
}
