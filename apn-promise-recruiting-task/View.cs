using apn_promise_recruiting_task.Controller;
using apn_promise_recruiting_task.Model;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace apn_promise_recruiting_task.View
{
    internal class View
    {
        private readonly Controller.Controller _controller;

        public View(Controller.Controller controller)
        {
            _controller = controller;
        }

        public void DisplayProducts()
        {
            var products = _controller.GetProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name}: {product.Price} {product.Currency}");
            }
        }
    }
}

