using System.Collections.Generic;

namespace Domain.Services
{
    public interface IParser<T>
    {
        IEnumerable<T> Parse(IEnumerable<string> records);
    }
}