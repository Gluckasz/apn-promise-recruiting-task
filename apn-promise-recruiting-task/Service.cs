using apn_promise_recruiting_task.Model;

namespace apn_promise_recruiting_task.Service
{
    public class Service
    {
        private readonly ApplicationDbContext _context;

        public Service(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return _context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching products", ex);
            }
        }
    }
}