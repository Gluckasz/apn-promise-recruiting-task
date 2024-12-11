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

        public int LoginOrRegistration()
        {
            Console.WriteLine("Dostępne są 2 operacje:");
            Console.WriteLine("1. Zarejestruj się");
            Console.WriteLine("2. Zaloguj się");
            Console.WriteLine("Wybierz jedną z tych operacji wpisując numer od 1 do 2");
            Console.WriteLine();
            string? operation = Console.ReadLine();
            if (operation == "1")
            {
                Registration();
            }
            else if (operation == "2")
            {
                return Login();
            }
            else
            {
                Console.WriteLine("Wpisz numer od 1 do 2");
            }
            return -1;
        }

        public void Registration()
        {
            Console.WriteLine("Podaj nazwę użytkownika");
            string? username = Console.ReadLine();
            Console.WriteLine("Podaj hasło");
            string? password = Console.ReadLine();
            if (username != null && password != null && username.Length != 0 && password.Length != 0)
            {
                try
                {
                    _controller.RegisterUser(username, password);
                    Console.WriteLine("Zarejestrowano pomyślnie");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Nie wpisano hasła lub nazwy użytkownika");
            }
            Console.WriteLine("Naciśnij dowolny przycisk aby kontynuować");
            Console.ReadLine();
        }

        public int Login()
        {
            Console.WriteLine("Podaj nazwę użytkownika");
            string? username = Console.ReadLine();
            Console.WriteLine("Podaj hasło");
            string? password = Console.ReadLine();
            int userId = -1;
            if (username != null && password != null && username.Length != 0 && password.Length != 0)
            {
                try
                {
                    userId = _controller.LoginUser(username, password);
                    Console.WriteLine("Zalogowano pomyślnie");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Nie wpisano hasła lub nazwy użytkownika");
            }
            Console.WriteLine("Naciśnij dowolny przycisk aby kontynuować");
            Console.ReadLine();
            return userId;
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
            Console.WriteLine("4. Wyloguj się");
            Console.WriteLine("5. Wyjście");
            Console.WriteLine("Wybierz jedną z tych operacji wpisując numer od 1 do 5.");
            Console.WriteLine();
        }

        public void ProcessOperations(ref int userId)
        {
            string? operation = Console.ReadLine();
            switch (operation)
            {
                case "1":
                    Console.WriteLine("Wpisz numer produktu do dodania");
                    string? productNumber = Console.ReadLine();
                    Console.WriteLine("Wpisz numer zamówienia do którego chcesz dodać produkt");
                    string? orderNumber = Console.ReadLine();
                    if (productNumber != null && orderNumber != null && productNumber.Length != 0 && orderNumber.Length != 0)
                    {
                        try
                        {
                            _controller.AddProductToOrder(Convert.ToInt32(productNumber), Convert.ToInt32(orderNumber), userId);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wpisz numer");
                    }
                    Console.WriteLine("Naciśnij dowolny przycisk aby kontynuować");
                    Console.ReadLine();
                    break;

                case "2":
                    Console.WriteLine("Wpisz numer zamówienia z którego chcesz odjąć produkt");
                    orderNumber = Console.ReadLine();
                    if (orderNumber != null && orderNumber.Length != 0)
                    {
                        try
                        {
                            var orderITems = _controller.GetAllOrderITemsFromOrder(Convert.ToInt32(orderNumber), userId);
                            if (orderITems != null && orderITems.Count > 0)
                            {
                                Console.WriteLine("Produkty w zamówieniu:");
                                foreach (var orderITem in orderITems)
                                {
                                    Console.WriteLine($"{orderITem.OrderITemId}. {orderITem.Product.Name}: {orderITem.Product.Price} {orderITem.Product.Currency}");
                                }
                                Console.WriteLine("Wpisz numer produktu który chcesz usunąć");
                                string? orderITemNumber = Console.ReadLine();
                                if (orderITemNumber != null && orderITemNumber.Length != 0)
                                {
                                    _controller.RemoveItemFromOrder(Convert.ToInt32(orderITemNumber), Convert.ToInt32(orderNumber), userId);
                                }
                                else
                                {
                                    Console.WriteLine("Wpisz numer");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Zamówienie jest puste");
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    Console.WriteLine("Naciśnij dowolny przycisk aby kontynuować");
                    Console.ReadLine();
                    break;

                case "3":
                    Console.WriteLine("Wpisz numer zamówienia, dla którego chcesz wyświetlić wartość");
                    orderNumber = Console.ReadLine();
                    if (orderNumber != null && orderNumber.Length != 0)
                    {
                        try
                        {
                            var orderITems = _controller.GetAllOrderITemsFromOrder(Convert.ToInt32(orderNumber), userId);
                            if (orderITems != null && orderITems.Count > 0)
                            {
                                Console.WriteLine("Produkty w zamówieniu:");
                                foreach (var orderITem in orderITems)
                                {
                                    Console.WriteLine($"{orderITem.OrderITemId}. {orderITem.Product.Name}: {orderITem.Product.Price} {orderITem.Product.Currency}");
                                }
                                Console.WriteLine($"Wartość zamówienia to: {Math.Round(_controller.GetOrderValue(Convert.ToInt32(orderNumber), userId), 2)} {orderITems.FirstOrDefault().Product.Currency}");
                            }
                            else
                            {
                                Console.WriteLine("Zamówienie jest puste");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    Console.WriteLine("Naciśnij dowolny przycisk aby kontynuować");
                    Console.ReadLine();
                    break;

                case "4":
                    userId = -1;
                    Console.WriteLine("Wylogowano");
                    Console.WriteLine("Naciśnij dowolny przycisk aby kontynuować");
                    Console.ReadLine();
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Wybierz numer od 1 do 4:");
                    Console.WriteLine("Naciśnij dowolny przycisk aby kontynuować");
                    Console.ReadLine();
                    break;
            }
        }
    }
}

