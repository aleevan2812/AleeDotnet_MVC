using AleeBook.Models;

namespace AleeBook.DataAccess.Respository.IRespository
{
    public interface IProductRepository : IRespository<Product>
    {
        void Update(Product obj);
    }
}