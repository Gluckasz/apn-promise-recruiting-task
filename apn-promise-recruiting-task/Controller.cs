using apn_promise_recruiting_task.Model;

namespace apn_promise_recruiting_task.Controller
{
    internal class Controller
    {
        private readonly Service.Service _productService;

        public Controller(Service.Service productService)
        {
            _productService = productService;
        }

        public List<Product> GetProducts()
        {
            return _productService.GetAllProducts();
        }

        public void AddProductToOrder(int productId, int orderId = 0)
        {
            _productService.AddProductToOrder(productId, orderId);
        }
    }
}