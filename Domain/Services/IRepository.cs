using Domain.Models;

namespace Domain.Services
{
    public interface IRepository<T>
    {
        void Save(T record);
    }
}