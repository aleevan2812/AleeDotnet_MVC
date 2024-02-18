namespace AleeBook.DataAccess.Respository.IRespository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        void Save();
    }
}