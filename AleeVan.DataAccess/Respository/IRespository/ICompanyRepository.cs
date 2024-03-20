using AleeBook.Models;

namespace AleeBook.DataAccess.Respository.IRespository
{
    public interface ICompanyRepository : IRespository<Company>
    {
        void Update(Company obj);
    }
}