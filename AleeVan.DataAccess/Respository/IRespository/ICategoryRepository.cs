using AleeBook.Models;

namespace AleeBook.DataAccess.Respository.IRespository
{
    public interface ICategoryRepository : IRespository<Category>
    {
        void Update(Category obj);

        void Save();
    }
}