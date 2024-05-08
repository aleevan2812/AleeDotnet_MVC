using AleeBook.DataAccess.Data;
using AleeBook.DataAccess.Repository.IRepository;
using AleeBook.Models;

namespace AleeBook.DataAccess.Repository;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _db;

    public OrderDetailRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    //public void Save()
    //{
    //    //throw new NotImplementedException();
    //    _db.SaveChanges();
    //}

    public void Update(OrderDetail obj)
    {
        //throw new NotImplementedException();
        _db.OrderDetails.Update(obj);
    }
}