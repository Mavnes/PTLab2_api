using Microsoft.EntityFrameworkCore;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PTLab2_api.Data.Repositories.Implimentations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ShopDbContext _context;

        public Repository(ShopDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
