using apn_promise_recruiting_task.Controller;
using apn_promise_recruiting_task.Model;
using apn_promise_recruiting_task.Service;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace OrderTests
{
    public class ControllerTests
    {
        private Controller _controller;
        private Mock<IService> _service;

        public ControllerTests()
        {
            SetupTestContext();
        }

        private void SetupTestContext()
        {
            SetupMockService();
            _controller = new Controller(_service.Object);
        }

        private void SetupMockService()
        {
            _service = new Mock<IService>();

            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Laptop", Price = 2500, Currency = "PLN" },
                new Product { ProductId = 2, Name = "Klawiatura", Price = 120, Currency = "PLN" },
                new Product { ProductId = 3, Name = "Mysz", Price = 90, Currency = "PLN" },
                new Product { ProductId = 4, Name = "Monitor", Price = 1000, Currency = "PLN" },
                new Product { ProductId = 5, Name = "Kaczka debuggująca", Price = 66, Currency = "PLN" }
            };
            _service.Setup(s => s.GetAllProducts()).Returns(products);

            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "password"
            };

            var order = new Order { User = user, UserId = user.UserId };

            var orderITems = new List<OrderITem>
            {
                new OrderITem { Order = order, Product = products[0]},
                new OrderITem { Order = order, Product = products[0]},
                new OrderITem { Order = order, Product = products[1]},
                new OrderITem { Order = order, Product = products[1]},
                new OrderITem { Order = order, Product = products[1]},
                new OrderITem { Order = order, Product = products[1]},
                new OrderITem { Order = order, Product = products[1]},
            };

            _service.Setup(s => s.GetAllOrderITemsFromOrder(1, 1)).Returns(orderITems);
        }

        [Fact]
        public void GetProducts_ShouldGetProductsList()
        {
            var result = _controller.GetProducts();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
            Assert.Contains(result, p => p.Name == "Laptop");
            Assert.Contains(result, p => p.Name == "Klawiatura");
        }

        [Fact]
        public void GetAllOrderITemsFromOrder_ShouldGetOrderITemsList()
        {
            var result = _controller.GetAllOrderITemsFromOrder(1, 1);

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);
            Assert.Contains(result, oi => oi.Product.Name == "Laptop");
            Assert.Contains(result, oi => oi.Product.Name == "Laptop");
        }

        [Fact]
        public void AddProductToOrder_ShouldCallServiceMethod()
        {
            _controller.AddProductToOrder(1, 1, 1);

            _service.Verify(s => s.AddProductToOrder(1, 1, 1), Times.Once);
        }

        [Fact]
        public void RemoveItemFromOrder_ShouldCallServiceMethod()
        {
            _controller.RemoveItemFromOrder(1, 1, 1);

            _service.Verify(s => s.RemoveItemFromOrder(1, 1, 1), Times.Once);
        }

        [Fact]
        public void GetOrderValue_ShouldApplyDiscountCorrectly()
        {
            var result = _controller.GetOrderValue(1, 1);

            Assert.Equal((2500 * 2 + 120 * 5 - 2500 * 0.1 - 120 * 0.2 - 120 * 0.1) * 0.95, result);
        }

        [Fact]
        public void RegisterUser_ShouldCallServiceMethod()
        {
            _controller.RegisterUser("newuser", "newpassword");

            _service.Verify(s => s.RegisterUser("newuser", "newpassword"), Times.Once);
        }

        [Fact]
        public void LoginUser_ShouldReturnUserId()
        {
            _service.Setup(s => s.LoginUser("testuser", "password")).Returns(1);

            var userId = _controller.LoginUser("testuser", "password");

            Assert.Equal(1, userId);
        }
    }
}
