namespace RTSADocs.Data.Services
{
    public interface IPageFileService : ICrudService<PageFile>
    {
        bool IsCleanable(string fileName);
    }
}
