using Bogus;

namespace MinimalAPIs.Repositories
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }

        //public bool IsValid()
        //{
        //    return 
        //        ProductId != 0 &&
        //        !string.IsNullOrWhiteSpace(ProductName) &&
        //        Price >= 0;
        //}
    }

    public interface IProductsRepository
    {
        bool CreateProduct(Product product);
        Product GetProductById(int id);
        bool UpdateProduct(int id, Product product);
        bool DeleteProduct(int id);
    }

    public class ProductsRepository : IProductsRepository
    {
        private List<Product> products;
        private readonly ILogger<IProductsRepository> _logger;

        public ProductsRepository(ILogger<IProductsRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            GenerateSeedData();
        }

        public bool CreateProduct(Product product)
        {
            var productNameExistsAlready = products.Any(p => p.ProductName == product.ProductName);
            if (productNameExistsAlready)
            {
                _logger.LogError("Product exists already!");
                return false;
            }

            products.Add(product);
            return true;
        }

        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(a => a.ProductId == id);
        }

        public bool UpdateProduct(int id, Product product)
        {
            var p = products.FirstOrDefault(a => a.ProductId == id);
            
            p.ProductName = product.ProductName;
            p.CreatedOn = DateTime.Now;
            p.Price = product.Price;

            return true;
        }

        public bool DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return false;
            }

            products.Remove(product);
            return true;
        }

        private void GenerateSeedData()
        {
            products = new Faker<Product>()
                .RuleFor(p => p.ProductId, p => p.IndexFaker)
                .RuleFor(p => p.ProductName, p => p.Random.Words(2))
                .RuleFor(p => p.Price, p => Convert.ToDouble(p.Finance.Amount()))
                .RuleFor(p => p.CreatedOn, p => p.Date.Recent())
                .Generate(50);
        }
    }
}
