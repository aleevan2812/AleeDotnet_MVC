using AleeVan.Models;

namespace AleeVan.DataAccess.Respository.IRespository
{
    public interface ICategoryRepository : IRespository<Category>
    {
        void Update(Category obj);

        void Save();
    }
}