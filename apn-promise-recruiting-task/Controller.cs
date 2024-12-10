using apn_promise_recruiting_task.Model;
using apn_promise_recruiting_task.Service;

namespace apn_promise_recruiting_task.Controller
{
    internal class Controller
    {
        private readonly ProductService _productService;

        public Controller(ProductService productService)
        {
            _productService = productService;
        }

        public List<Product> GetProducts()
        {
            return _productService.GetAllProducts();
        }
    }
}