using tparf.Models;

namespace tparf.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(Guid userId);
        ICollection<Order> GetOrdersByUser(Guid userId);
        ICollection<Product> GetProductByUser(Guid userId);
        bool UserExists(Guid userId);
        bool CreateUser(User user);
        bool Save();
    }
}
