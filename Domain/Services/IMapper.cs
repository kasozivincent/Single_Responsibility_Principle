using System.Collections.Generic;

namespace Domain.Services
{
    public interface IMapper
    {
        TradeRecord Map(IList<string> fields);
    }
}