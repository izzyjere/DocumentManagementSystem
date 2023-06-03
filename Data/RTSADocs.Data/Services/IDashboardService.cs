namespace RTSADocs.Data.Services
{
    public interface IDashboardService
    {
        int Count<TModel>() where TModel : class, IEntity;
    }
}