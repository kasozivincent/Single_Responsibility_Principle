using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
    public interface IRepository
    {
        void Save(IList<Traderecord> record);
    }
}