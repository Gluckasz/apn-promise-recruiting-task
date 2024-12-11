using apn_promise_recruiting_task.Model;

namespace apn_promise_recruiting_task.Service
{
    public interface IService
    {
        List<Product> GetAllProducts();
        List<OrderITem> GetAllOrderITemsFromOrder(int orderId, int userId);
        void AddProductToOrder(int productId, int orderId, int userId);
        void RemoveItemFromOrder(int orderITemId, int orderId, int userId);
        void RegisterUser(string username, string password);
        int LoginUser(string username, string password);
    }
}