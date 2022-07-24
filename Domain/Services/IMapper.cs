using System.Collections.Generic;

namespace Domain.Services
{
    public interface IMapper<T>
    {
        T Map(IEnumerable<string> fields);
    }
}