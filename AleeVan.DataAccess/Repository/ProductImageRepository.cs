using AleeBook.DataAccess.Data;
using AleeBook.DataAccess.Repository.IRepository;
using AleeBook.DataAccess.Respository;
using AleeBook.Models;

namespace AleeBook.DataAccess.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private ApplicationDbContext _db;

        public ProductImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    //throw new NotImplementedException();
        //    _db.SaveChanges();
        //}

        public void Update(ProductImage obj)
        {
            //throw new NotImplementedException();
            _db.ProductImages.Update(obj);
        }
    }
}