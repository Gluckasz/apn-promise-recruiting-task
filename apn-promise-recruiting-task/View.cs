using apn_promise_recruiting_task.Model;

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
                Console.WriteLine($"{product.ProductId}. {product.Name}: {product.Price} {product.Currency}");
            }
            Console.WriteLine();
        }

        public void DisplayOperations()
        {
            Console.WriteLine("Dostępne są 4 operacje:");
            Console.WriteLine("1. Dodanie produktu");
            Console.WriteLine("2. Usunięcie produktu");
            Console.WriteLine("3. Wyświetlenie wartości zamówienia");
            Console.WriteLine("4. Wyjście");
            Console.WriteLine("Wybierz jedną z tych operacji wpisując numer od 1 do 4.");
            Console.WriteLine();
        }
        public void ProcessOperations()
        {
            string? operation = Console.ReadLine();
            switch (operation)
            {
                case "1":
                    // Add product
                    break;

                case "2":
                    // Remove product
                    break;

                case "3":
                    // Display order value
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Wybierz numer od 1 do 4:");
                    break;
            }
        }
    }
}

