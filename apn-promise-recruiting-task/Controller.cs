using apn_promise_recruiting_task.Model;
using Microsoft.EntityFrameworkCore;

namespace apn_promise_recruiting_task.Controller
{
    internal class Controller
    {
        private readonly Service.Service _service;

        public Controller(Service.Service service)
        {
            _service = service;
        }

        public List<Product> GetProducts()
        {
            return _service.GetAllProducts();
        }

        public List<OrderITem> GetAllOrderITemsFromOrder(int orderId)
        {
            return _service.GetAllOrderITemsFromOrder(orderId);
        }

        public void AddProductToOrder(int productId, int orderId)
        {
            _service.AddProductToOrder(productId, orderId);
        }

        public void RemoveItemFromOrder(int orderITemId, int orderId)
        {
            _service.RemoveItemFromOrder(orderITemId, orderId);
        }
    }
}