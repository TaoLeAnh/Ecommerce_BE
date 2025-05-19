using EcommerceBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcommerceBackend.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> GetAllByUserId(string id)
        {
            return await _dbSet.Where(e => EF.Property<string>(e, "UserId") == id).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllByProductId(string id)
        {
            return await _dbSet.Where(e => EF.Property<string>(e, "ProductId") == id).ToListAsync();
        }
        
          public async Task<IEnumerable<T>> GetAllByPropertyAsync(string propertyName, object value)
    {
        // Use reflection to create a dynamic expression
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant(value);
        
        // Need to handle type conversion for the comparison
        Expression equalityExpression;
        if (property.Type != constant.Type)
        {
            var convertedConstant = Expression.Convert(constant, property.Type);
            equalityExpression = Expression.Equal(property, convertedConstant);
        }
        else
        {
            equalityExpression = Expression.Equal(property, constant);
        }
        
        var lambda = Expression.Lambda<Func<T, bool>>(equalityExpression, parameter);

        return await _dbSet.Where(lambda).ToListAsync();
    }
    }
}