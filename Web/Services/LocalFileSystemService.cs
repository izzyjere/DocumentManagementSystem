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
using Hangfire;

namespace RTSADocs.Services
{
    internal class LocalFileSystemService : IFileSystemService
    {
        // Root directory for the main file store
        readonly IPageFileService pageFileService;
        readonly ILogger<LocalFileSystemService> logger;
        public string FileSystemRootMain => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Main";

        // Root directory for the archive file store
        public string FileSystemRootArchive => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Archive";

        public LocalFileSystemService(IPageFileService pageFileService, ILogger<LocalFileSystemService> logger)
        {
            Init();
            this.pageFileService=pageFileService;
            this.logger=logger;
        }

        // Initialize the file system by creating the main and archive directories if they don't exist
        private void Init()
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
        public static List<string> GetFilesRecursive(string directory)
        {
            List<string> fileList = new List<string>();
            HashSet<string> addedFiles = new HashSet<string>();

            try
            {
                // Process files in the current directory
                string[] files = Directory.GetFiles(directory);
                foreach (string file in files)
                {
                    if (!addedFiles.Contains(file))
                    {
                        fileList.Add(file);
                        addedFiles.Add(file);
                    }
                }

                // Recursively process subdirectories
                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    fileList.AddRange(GetFilesRecursive(subdirectory));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing directory: {ex.Message}");
            }

            return fileList;
        }
        public Task FileStoreCleanUp()
        {
            logger.LogInformation($"Filestore cleanup triggered at {DateTime.Now:dd/MM/yyy H:mm:ss}");
            var mainFileStoreFiles =GetFilesRecursive(FileSystemRootMain);
            var archieveFiles = GetFilesRecursive(FileSystemRootArchive);
            logger.LogInformation("Found: {0} files in main store, Found: {1} files in archive store.",mainFileStoreFiles.Count,archieveFiles.Count);   
           var mainCleaner = Task.Run(async () =>
            {
                foreach (var filePath in mainFileStoreFiles)
                {
                    var fileName = Path.GetFileName(filePath);
                    if (fileName!=null && await pageFileService.IsCleanable(fileName))
                    {
                        File.Delete(filePath);
                        logger.LogInformation("Cleaned Up: {0}", filePath);
                    }
                }
            });
            var archiveCleaner = Task.Run(async () =>
            {
                foreach (var filePath in archieveFiles)
                {
                    var fileName = Path.GetFileName(filePath);
                    if (fileName!=null && await pageFileService.IsCleanable(fileName))
                    {
                        File.Delete(filePath);
                        logger.LogInformation("Cleaned Up: {0}", filePath);
                    }
                }
            });
            return Task.WhenAll(mainCleaner, archiveCleaner);
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
