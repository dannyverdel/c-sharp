using System;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CSharpDemos.ClassLibrary.DesignPatterns.RepositoryPattern;

/*
 * The repository design pattern is a software design pattern that helps to seperte the data
 * access logic from the rest of the application's code. It provides an abstraction layer between
 * the data source and the application's business logic. This abstraction layer simplifies the code
 * by providing a standard set of methods that can be used to perform CRUD operations on the data
 * source, without exposing the implementation details of the data access layer to the rest of the
 * application.
 * 
 * In simplet terms, the repository pattern helps to organize code related to data access and makes 
 * it easier to maintain and test.
 * 
 * In this example, the 'ProductRepository' class implements the 'IProductRepository' interface, 
 * which defines the standard set of methods for performing CRUD operations on products. The 
 * 'ProductService' class uses the 'IProductRepository' interface to perform business logic on 
 * products, without needing to know the implementation details of the data access layer. This 
 * seperation of concerns makes the code easier to maintain and test.
 */

public class InvokeRepositoryPattern : IInvokeMethod
{
    private readonly ILogger<InvokeRepositoryPattern> _logger;
    private readonly IConfiguration _config;

    public InvokeRepositoryPattern(ILogger<InvokeRepositoryPattern> logger, IConfiguration config) {
        _logger = logger;
        _config = config;
    }

    public void InvokeMethod() {
        IProductRepository product_repository = new ProductRepository();

        ProductService product_service = new ProductService(product_repository);
        Product product = new Product { ArticleNumber = 3543290 };

        _logger.LogInformation("Adding product");
        product_service.AddProduct(product);

        product_service.GetAllProducts().Dump();

        _logger.LogInformation("Updating product");
        product.ArticleNumber = 9023453;
        product_service.UpdateProduct(product);

        product_service.GetAllProducts().Dump();

        _logger.LogInformation("Deleting product");
        product_service.DeleteProduct(product);

        product_service.GetAllProducts().Dump();

        _logger.LogInformation("Getting one product");
        product_service.GetProductById(1).Dump();
    }
}

public interface IProductRepository
{
    Product? GetProductById(int id);
    IEnumerable<Product> GetAllProducts();
    void AddProduct(Product product);
    Product? UpdateProduct(Product updated_product);
    Product? DeleteProduct(Product deleted_product);
}

public class ProductRepository : IProductRepository
{
    private static readonly List<Product> _products = new ProductFaker().Generate(2);

    public Product? GetProductById(int id) => _products.Where(x => x.Id == id).FirstOrDefault();
    public IEnumerable<Product> GetAllProducts() => _products;
    public void AddProduct(Product product) {
        int id = _products.Count;
        product.Id = id + 1;
        _products.Add(product);
    }
    public Product? UpdateProduct(Product updated_product) {
        int index = _products.FindIndex(x => x.Id == updated_product.Id);
        if ( index < 0 )
            return null;

        _products[index].ArticleNumber = updated_product.ArticleNumber;

        return _products[index];
    }
    public Product? DeleteProduct(Product deleted_product) {
        int index = _products.FindIndex(x => x.Id == deleted_product.Id);
        if ( index < 0 )
            return null;

        _products.Remove(deleted_product);

        return deleted_product;
    }
}

public class ProductService
{
    private readonly IProductRepository _product_repository;

    public ProductService(IProductRepository product_repository) => _product_repository = product_repository;

    // business logic comes before each _product_repository call
    public Product? GetProductById(int id) => _product_repository.GetProductById(id);
    public IEnumerable<Product> GetAllProducts() => _product_repository.GetAllProducts();
    public void AddProduct(Product product) => _product_repository.AddProduct(product);
    public Product? UpdateProduct(Product product) => _product_repository.UpdateProduct(product);
    public Product? DeleteProduct(Product product) => _product_repository.DeleteProduct(product);
}

public class Product
{
    public int Id { get; set; }
    public int ArticleNumber { get; set; }
}

public class ProductFaker : Faker<Product>
{
    public ProductFaker() {
        int id = 1;
        UseSeed(Random.Shared.Next(1, 1000000))
            .RuleFor(x => x.Id, _ => id++)
            .RuleFor(x => x.ArticleNumber, _ => Random.Shared.Next(1000000, 10000000));
    }
}

