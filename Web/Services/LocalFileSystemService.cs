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
        readonly IPageFileService pageFileService;
        readonly ILogger<LocalFileSystemService> logger;
        public string FileSystemRootMain => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Main";
        public string FileSystemRootArchive => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Archive";

        public LocalFileSystemService(IPageFileService pageFileService, ILogger<LocalFileSystemService> logger)
        {
            Init();
            this.pageFileService=pageFileService;
            this.logger=logger;
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

                await Task.Run(() =>
                {
                    memoryStream.WriteTo(fileStream);
                    fileStream.Close();
                });

                return Result<string>.Success(Path.Combine(path, fileName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+e.StackTrace);
                return Result<string>.Failure("An error occured while trying to write file to filestore.");
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
           var mainCleaner = Task.Run(() =>
            {
                foreach (var filePath in mainFileStoreFiles)
                {
                    var fileName = Path.GetFileName(filePath);
                    if (fileName!=null && pageFileService.IsCleanable(fileName))
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
                var currentPath = Path.Combine(FileSystemRootArchive + filePath);
                var newPath = Path.Combine(FileSystemRootMain + filePath);
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
