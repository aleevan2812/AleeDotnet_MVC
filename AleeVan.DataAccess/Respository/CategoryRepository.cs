using AleeBook.DataAccess.Data;
using AleeBook.DataAccess.Respository.IRespository;
using AleeBook.Models;

namespace AleeBook.DataAccess.Respository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    //throw new NotImplementedException();
        //    _db.SaveChanges();
        //}

        public void Update(Category obj)
        {
            //throw new NotImplementedException();
            _db.Categories.Update(obj);
        }
    }
}