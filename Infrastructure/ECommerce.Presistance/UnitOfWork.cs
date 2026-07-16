using ECommerce.Contract;
using ECommerce.Contract.Repositories;

namespace ECommerce.Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepository;
        private readonly AppDbContext _context;
        public UnitOfWork(IUserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        public IUserRepository UserRepository => _userRepository;
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
