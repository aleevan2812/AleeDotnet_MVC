using AleeBook.DataAccess.Data;
using AleeBook.DataAccess.Respository.IRespository;
using AleeBook.Models;

namespace AleeBook.DataAccess.Respository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    //throw new NotImplementedException();
        //    _db.SaveChanges();
        //}

        public void Update(Product obj)
        {
            //throw new NotImplementedException();
            _db.Products.Update(obj);
        }
    }
}