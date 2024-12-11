using apn_promise_recruiting_task.Controller;
using apn_promise_recruiting_task.Model;
using apn_promise_recruiting_task.Service;
using apn_promise_recruiting_task.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<ApplicationDbContext>()
            .AddTransient<Service>()
            .AddTransient<Controller>()
            .AddTransient<View>()
            .BuildServiceProvider();

        var context = serviceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();

        var view = serviceProvider.GetService<View>();
        int userId = -1;
        while (true)
        {
            while (userId == -1)
            {
                userId = view.LoginOrRegistration();
                Console.Clear();
            }
            while (userId != -1)
            {
                view.DisplayProducts();
                view.DisplayOperations();
                view.ProcessOperations(ref userId);
                Console.Clear();
            }
        }
    }
}
