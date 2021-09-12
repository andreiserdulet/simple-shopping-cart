using Data.Abstraction;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntityModel
        {
            return new Repository<T>(this._databaseContext);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this._databaseContext.SaveChangesAsync() > 0;
        }
    }
}
