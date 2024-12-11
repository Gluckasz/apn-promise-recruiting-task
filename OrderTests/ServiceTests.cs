using apn_promise_recruiting_task.Model;
using apn_promise_recruiting_task.Service;
using Microsoft.EntityFrameworkCore;

namespace OrderTests
{
    public class ServiceTests
    {
        private ApplicationDbContext _testContext;
        private Service _service;

        public ServiceTests()
        {
            SetupTestContext();
        }

        private void SetupTestContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _testContext = new ApplicationDbContext(options);

            SeedTestData();

            _service = new Service(_testContext);
        }

        private void SeedTestData()
        {
            _testContext.Products.RemoveRange(_testContext.Products);

            _testContext.Products.AddRange(new List<Product>
            {
                new Product { ProductId = 1, Name = "Laptop", Price = 2500, Currency = "PLN" },
                new Product { ProductId = 2, Name = "Klawiatura", Price = 120, Currency = "PLN" },
                new Product { ProductId = 3, Name = "Mysz", Price = 90, Currency = "PLN" },
                new Product { ProductId = 4, Name = "Monitor", Price = 1000, Currency = "PLN" },
                new Product { ProductId = 5, Name = "Kaczka debuggująca", Price = 66, Currency = "PLN" }
            });

            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "password"
            };
            _testContext.Users.Add(user);
            _testContext.SaveChanges();

            _testContext.SaveChanges();
        }

        [Fact]
        public void GetAllProducts_ReturnsAllProducts()
        {
            var result = _service.GetAllProducts();

            Assert.Equal(5, result.Count);
            Assert.Contains(result, p => p.Name == "Laptop");
            Assert.Contains(result, p => p.Name == "Klawiatura");
        }

        [Fact]
        public void GetAllItemsFromOrder_OneItemInOrder_GetsTheItem()
        {
            _service.AddProductToOrder(1, 1, 1);

            var result = _service.GetAllOrderITemsFromOrder(1, 1);

            Assert.Equal(1, result.Count);
            Assert.Contains(result, p => p.OrderITemId == 1);
        }

        [Fact]
        public void GetAllItemsFromOrder_OrderIsNull_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => _service.GetAllOrderITemsFromOrder(1, 1));
        }

        [Fact]
        public void AddProductToOrder_NewOrder_AddsProductSuccessfully()
        {
            _service.AddProductToOrder(1, 1, 1);

            var order = _testContext.Orders
                .FirstOrDefault(o => o.OrderId == 1 && o.UserId == 1);

            Assert.NotNull(order);
            Assert.Single(order.OrderITems);
            Assert.Equal(1, order.OrderITems[0].ProductId);
        }

        [Fact]
        public void AddProductToOrder_ProductIsNull_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => _service.AddProductToOrder(6, 1, 1));
        }

        [Fact]
        public void AddProductToOrder_OrderIdIsNegative_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => _service.AddProductToOrder(1, -1, 1));
        }

        [Fact]
        public void AddProductToOrder_UserIsNull_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => _service.AddProductToOrder(1, 1, 2));
        }

        [Fact]
        public void AddProductToOrder_OrderBelongsToOtherUser_ShouldThrowException()
        {
            _service.AddProductToOrder(1, 1, 1);
            _service.RegisterUser("test", "testPassword");
            var result = _service.LoginUser("test", "testPassword");
            Assert.Throws<Exception>(() => _service.AddProductToOrder(1, 1, 2));
        }

        [Fact]
        public void RemoveProductFromOrder_NewOrder_RemovesProductSuccessfully()
        {
            _service.AddProductToOrder(1, 1, 1);

            _service.RemoveItemFromOrder(1, 1, 1);

            var order = _testContext.Orders
                .FirstOrDefault(o => o.OrderId == 1 && o.UserId == 1);

            Assert.NotNull(order);
            Assert.Empty(order.OrderITems);
        }

        [Fact]
        public void RemoveProductFromOrder_OrderITemIsNull_ShouldThrowException()
        {
            _service.AddProductToOrder(1, 1, 1);
            Assert.Throws<Exception>(() => _service.RemoveItemFromOrder(2, 1, 1));
        }

        [Fact]
        public void RemoveProductFromOrder_OrderIsNull_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => _service.RemoveItemFromOrder(1, 1, 1));
        }

        [Fact]
        public void RemoveProductFromOrder_OrderBelongsToOtherUser_ShouldThrowException()
        {
            _service.AddProductToOrder(1, 1, 1);
            _service.RegisterUser("test", "testPassword");
            var result = _service.LoginUser("test", "testPassword");
            Assert.Throws<Exception>(() => _service.RemoveItemFromOrder(1, 1, 2));
        }

        [Fact]
        public void RegisterUser_NewUser_ShouldBeRegisteredSuccessfully()
        {
            _service.RegisterUser("test", "testPassword");
            var user = _testContext.Users
                .FirstOrDefault(u => u.Username == "test" && u.Password == "testPassword");
            Assert.NotNull(user);
            Assert.Equal(2, user.UserId);
        }

        [Fact]
        public void RegisterUser_UserExists_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => _service.RegisterUser("testuser", "testpassword"));
        }

        [Fact]
        public void LoginUser_ExistingUser_ShouldLoginSuccessfully()
        {
            var result = _service.LoginUser("testuser", "password");
            Assert.Equal(1, result);
        }

        [Fact]
        public void LoginUser_UserNotExists_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => _service.LoginUser("nonexistinguser", "nonexistingpassword"));
        }

        [Fact]
        public void LoginUser_WrongPassword_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => _service.LoginUser("testuser", "nonexistingpassword"));
        }
    }
}