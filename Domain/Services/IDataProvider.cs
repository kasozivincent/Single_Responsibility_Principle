using System.Collections.Generic;

namespace Domain.Services
{
    public interface IDataProvider
    {
        IEnumerable<string> Read();
    }

}