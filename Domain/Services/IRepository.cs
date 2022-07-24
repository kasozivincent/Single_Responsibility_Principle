using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
    public interface IRepository<T>
    {
        void Save(IEnumerable<T> record);
    }
}