using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
    public interface IParser
    {
        IList<Traderecord> Parse(IEnumerable<string> records);
    }
}