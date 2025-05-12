using EcommerceBackend.Repositories;
using EcommerceBackend.Models;

namespace EcommerceBackend.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<User> Users { get; private set; }
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Order> Orders { get; private set; }
        public IGenericRepository<Review> Reviews { get; private set; }
        public IGenericRepository<OrderDetail> OrderDetails { get; private set; }
        public IGenericRepository<Payment> Payments { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(context);
            Products = new GenericRepository<Product>(context);
            Categories = new GenericRepository<Category>(context);
            Orders = new GenericRepository<Order>(context);
            Reviews = new GenericRepository<Review>(context);
            OrderDetails = new GenericRepository<OrderDetail>(context);
            Payments = new GenericRepository<Payment>(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}