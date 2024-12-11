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

        public List<OrderITem> GetAllOrderITemsFromOrder(int orderId, int userId)
        {
            return _service.GetAllOrderITemsFromOrder(orderId, userId);
        }

        public void AddProductToOrder(int productId, int orderId, int userId)
        {
            _service.AddProductToOrder(productId, orderId, userId);
        }

        public void RemoveItemFromOrder(int orderITemId, int orderId, int userId)
        {
            _service.RemoveItemFromOrder(orderITemId, orderId, userId);
        }

        public double GetOrderValue(int orderId, int userId)
        {
            var orderITems = GetAllOrderITemsFromOrder(orderId, userId);
            var prices = orderITems.Select(o => o.Product.Price).ToList();

            double discount = 0;
            double sum = prices.Sum();
            prices.Sort();
            prices.Reverse();
            for (int i = 0; i < prices.Count; i += 2)
            {
                if (i + 2 < prices.Count)
                {
                    if (prices[i + 2] * 0.2 >= prices[i + 1] * 0.1)
                    {
                        discount += prices[i + 2] * 0.2;
                        i += 1;
                    }
                    else
                    {
                        discount += prices[i + 1] * 0.1;
                    }
                }
                else if (i + 1 < prices.Count)
                {
                    discount += prices[i + 1] * 0.1;
                }

            }
            if (sum > 5000)
            {
                discount += (sum - discount) * 0.05;
            }

            return sum - discount;
        }

        public void RegisterUser(string username, string password)
        {
            _service.RegisterUser(username, password);
        }

        public int LoginUser(string username, string password)
        {
            return _service.LoginUser(username, password);
        }
    }
}