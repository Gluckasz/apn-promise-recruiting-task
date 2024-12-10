using apn_promise_recruiting_task.Model;
using Microsoft.EntityFrameworkCore;

namespace apn_promise_recruiting_task.View;

internal class Program
{
    static void Main(string[] args)
    {
        using (var context = new ApplicationDbContext())
        {
            context.Database.Migrate();
        }
        Console.WriteLine("Hello World!");
    }
}

