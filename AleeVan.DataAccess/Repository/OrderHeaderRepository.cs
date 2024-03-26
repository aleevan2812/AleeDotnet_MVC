using AleeBook.DataAccess.Data;
using AleeBook.DataAccess.Repository.IRepository;
using AleeBook.DataAccess.Respository;
using AleeBook.Models;

namespace AleeBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    //throw new NotImplementedException();
        //    _db.SaveChanges();
        //}

        public void Update(OrderHeader obj)
        {
            //throw new NotImplementedException();
            _db.OrderHeaders.Update(obj);
        }
    }
}