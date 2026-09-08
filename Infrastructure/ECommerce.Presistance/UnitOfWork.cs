using ECommerce.Contract;
using ECommerce.Contract.Repositories;

namespace ECommerce.Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private ICategoryRepository _categoryRepository;
        private IBrandRepository _brandRepository;
        private readonly AppDbContext _context;
        public UnitOfWork(IUserRepository userRepository, AppDbContext context, ICategoryRepository categoryRepository, IBrandRepository brandRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _context = context;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _roleRepository = roleRepository;
        }
        public IUserRepository UserRepository => _userRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IBrandRepository BrandRepository => _brandRepository;
        public IRoleRepository RoleRepository => _roleRepository;
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
