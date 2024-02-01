using AleeVan.DataAccess.Data;
using AleeVan.DataAccess.Respository.IRespository;
using AleeVan.Models;

namespace AleeVan.DataAccess.Respository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            //throw new NotImplementedException();
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            //throw new NotImplementedException();
            _db.Categories.Update(obj);
        }
    }
}