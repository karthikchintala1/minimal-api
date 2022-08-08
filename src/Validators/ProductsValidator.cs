using FluentValidation;
using MinimalAPIs.Repositories;

namespace MinimalAPIs.Validators
{
    public class ProductsValidator : AbstractValidator<Product>
    {
        public ProductsValidator()
        {
            RuleFor(a => a.ProductName).NotEmpty();
            RuleFor(a => a.Price).GreaterThan(0);
            RuleFor(a => a.ProductId).GreaterThan(0);
        }
    }
}
