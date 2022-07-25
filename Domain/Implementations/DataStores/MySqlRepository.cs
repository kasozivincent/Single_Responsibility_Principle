using System.Collections.Generic;
using Domain.Models;
using Domain.Services;

namespace Domain.Implementations.DataStores
{
    public class MySqlRepository : IRepository
    {
        private readonly ILogger logger;

        public MySqlRepository(ILogger logger)
            => this.logger = logger;
        public void Save(IList<Traderecord> records)
        {
            
            srpContext dbcontext = new srpContext();
            dbcontext.Traderecords.AddRange(records);
            dbcontext.SaveChanges();
            logger.Log($"{records.Count} Records saved successfully");
        }
    }
}