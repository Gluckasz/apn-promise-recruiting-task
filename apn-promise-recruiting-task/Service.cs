using apn_promise_recruiting_task.Model;
using Microsoft.EntityFrameworkCore;

namespace apn_promise_recruiting_task.Service
{
    public class Service : IService
    {
        private readonly ApplicationDbContext _context;

        public Service(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }


        public List<OrderITem> GetAllOrderITemsFromOrder(int orderId, int userId)
        {
            var order = _context.Orders.Include(o => o.OrderITems).FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);
            if (order == null)
            {
                throw new Exception($"Zamówienie z id: {orderId} nie istnieje");
            }
            return order.OrderITems;
        }

        public void AddProductToOrder(int productId, int orderId, int userId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                throw new Exception($"Produkt z id: {productId} nie istnieje");
            }

            if (orderId <= 0)
            {
                throw new Exception("Id zamówienia musi być większe od 0");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception($"Użytkownik z id: {userId} nie istnieje");
            }

            if (!_context.Orders.Any(o => o.OrderId == orderId))
            {
                _context.Orders.Add(new Order { OrderId = orderId, User = user, UserId = userId });
                _context.SaveChanges();
            }

            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order.UserId != userId)
            {
                throw new Exception($"Zamówienie z id: {orderId} nie należy do użytkownika z id: {userId}");
            }
            if (order.OrderITems == null)
            {
                order.OrderITems = new List<OrderITem>();
            }
            var orderItem = new OrderITem
            {
                ProductId = productId,
                Product = product,
                OrderId = orderId,
                Order = order
            };

            order.OrderITems.Add(orderItem);

            _context.SaveChanges();
        }

        public void RemoveItemFromOrder(int orderITemId, int orderId, int userId)
        {
            var order = _context.Orders.Include(o => o.OrderITems).FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception($"Zamówienie z id: {orderId} nie istnieje");
            }

            var orderITem = _context.OrderITems.Find(orderITemId);
            if (orderITem == null)
            {
                throw new Exception($"Element zamówienia z id: {orderITem} nie istnieje");
            }

            if (order.UserId != userId)
            {
                throw new Exception($"Zamówienie z id: {orderId} nie należy do użytkownika z id: {userId}");
            }

            order.OrderITems.Remove(orderITem);
            _context.OrderITems.Remove(orderITem);
            _context.SaveChanges();
        }

        public void RegisterUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                throw new Exception($"Użytkownik o nazwie: {username} już istnieje");
            }

            _context.Users.Add(new User { Username = username, Password = password });
            _context.SaveChanges();
        }

        public int LoginUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new Exception($"Użytkownik o nazwie: {username} nie istnieje");
            }

            return user.UserId;
        }
    }
}