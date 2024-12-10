using apn_promise_recruiting_task.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apn_promise_recruiting_task.Service
{
    public class ProductService
    {
        public List<Product> GetAllProducts()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Products.ToList();
            }
        }
    }
}
