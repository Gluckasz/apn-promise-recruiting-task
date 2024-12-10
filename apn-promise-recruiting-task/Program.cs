using apn_promise_recruiting_task;
using Microsoft.EntityFrameworkCore;

using (var context = new ApplicationDbContext())
{
    context.Database.Migrate();
}