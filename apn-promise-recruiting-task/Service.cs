using apn_promise_recruiting_task.Model;
using Microsoft.EntityFrameworkCore;

namespace apn_promise_recruiting_task.Service
{
    public class Service
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

        public void AddProductToOrder(int productId, int orderId = 0)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                throw new Exception("Can't find product with specified id");
            }

            if (!_context.Orders.Any(o => o.OrderId == orderId))
            {
                _context.Orders.Add(new Order { OrderId = orderId });
                _context.SaveChanges();
            }

            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

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

            _context.OrderITems.Add(orderItem);

            order.OrderITems.Add(orderItem);

            _context.SaveChanges();
        }
    }
}