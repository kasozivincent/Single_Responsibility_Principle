using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
    public interface IParser
    {
        IEnumerable<Traderecord> Parse(IEnumerable<string> records);
    }
}