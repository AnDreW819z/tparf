using AutoMapper;
using tparf.Data;
using tparf.Interfaces;
using tparf.Models;

namespace tparf.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public ICollection<Order> GetOrdersByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(Guid userId)
        {
            return _context.Users.Where(r => r.Id == userId).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UserExists(Guid userId)
        {
            return _context.Users.Any(o => o.Id == userId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Product> GetProductByUser(Guid userId)
        {
            return _context.Orders.Where(p => p.User.Id == userId).Select(p => p.Product).ToList();
        }
    }
}
