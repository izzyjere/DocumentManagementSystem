using RTSADocs.Data;
using RTSADocs.Data.Services;
using RTSADocs.Shared.DTOs;
using RTSADocs.Shared.Models;
using RTSADocs.Shared.Services;

namespace RTSADocs.Services
{
    internal class LocalFileSystemService  : IFileSystemService
    {
        public string FileSystemRootMain => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Main";
        public string FileSystemRootArchive => "C:\\ProgramData\\Codelabs\\DMS\\FileStores\\Archive";
      
        public LocalFileSystemService()
        {
           Init();
        }
        private void Init()
        {
            if(!Directory.Exists(FileSystemRootMain))
            {
                Directory.CreateDirectory(FileSystemRootMain);
            }
            if(!Directory.Exists(FileSystemRootArchive))
            {
                Directory.CreateDirectory(FileSystemRootArchive);
            }
        }
        public Task<string> InitializeFileStore()
        {
            var folderName = GenerateRandomStructure();
            var fullPath = Path.Combine(FileSystemRootMain, folderName);
            Directory.CreateDirectory(fullPath);
            return Task.FromResult(folderName);
        }
        private string GenerateRandomStructure(int maxDepth = 6)
        {
            var depth = new Random().Next(2, maxDepth);
            var finalStructure = "";
            for (int i = 0; i < depth; i++)
            {
                finalStructure += $"/{Guid.NewGuid().ToString()[..8]}";
            }
            return finalStructure;
        }
        public async Task<Result<string>> WriteFileToStore(MemoryStream memoryStream, string fileStore)
        {
            try
            {
                var randomFolder = GenerateRandomStructure(4);
                var fileName = Guid.NewGuid().ToString() + ".rtsadoc";
                var filePath = Path.Combine(fileStore, randomFolder, fileName);
                var fileStreem = File.OpenWrite(Path.Combine(FileSystemRootMain, filePath));
                await memoryStream.CopyToAsync(fileStreem);
                return Result<string>.Success(filePath);
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
                var currentPath = Path.Combine(FileSystemRootMain, filePath);
                var newPath = Path.Combine(FileSystemRootArchive, filePath);
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
            if(File.Exists(finalPath))
            {
                var fileStream = File.OpenRead(finalPath);
                await fileStream.CopyToAsync(memoryStream);
                return Result<MemoryStream>.Success(memoryStream);
            }
            return Result<MemoryStream>.Failure("File not found.");
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
