namespace DMS.Data.Services
{
    public interface IPageFileService : ICrudService<PageFile>
    {
        Task<bool> IsCleanable(string fileName);
    }
}
