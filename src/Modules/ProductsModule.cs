using Carter;
using Carter.ModelBinding;
using MinimalAPIs.Repositories;

namespace MinimalAPIs.Modules
{
    public class ProductsModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/product/create", CreateProduct);

            app.MapGet("/product/{id}", GetProductById);

            app.MapPut("/product/update/{id}", UpdateProduct);

            app.MapDelete("/product/delete/{id}", DeleteProduct);
        }

        private IResult CreateProduct(HttpContext context, Product product, IProductsRepository productsRepo)
        {
            var result = context.Request.Validate(product);
            if (!product.IsValid()) return Results.BadRequest();
            productsRepo.CreateProduct(product);
            return Results.StatusCode(201);
        }

        private IResult GetProductById(int id, IProductsRepository repo)
        {
            if (id <= 0) return Results.BadRequest();

            return Results.Ok(repo.GetProductById(id));
        }

        private IResult UpdateProduct(int id, Product product, IProductsRepository productRepo)
        {
            if (!product.IsValid()) return Results.BadRequest();

            productRepo.UpdateProduct(id, product);
            return Results.Ok();
        }

        private IResult DeleteProduct(int id, IProductsRepository repo)
        {
            return Results.Ok(repo.DeleteProduct(id));
        }
    }
}
