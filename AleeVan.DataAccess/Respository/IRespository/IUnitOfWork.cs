namespace AleeBook.DataAccess.Respository.IRespository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }

        void Save();
    }
}