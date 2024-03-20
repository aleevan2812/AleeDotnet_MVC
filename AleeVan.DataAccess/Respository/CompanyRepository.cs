using AleeBook.DataAccess.Data;
using AleeBook.DataAccess.Respository.IRespository;
using AleeBook.Models;

namespace AleeBook.DataAccess.Respository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            //throw new NotImplementedException();
            _db.Update(obj);
        }
    }
}