using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RMS.Models;
using RMS.Repository;

namespace RMS.Services;

public class ProductionService : IProductionService
{
    public ProductionService(IProductRepository productRepository, IBrandRepository brandRepository,
     ICategoryRepository categoryRepository, IStockRepository stockRepository, IStoreRepository storeRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
        _stockRepository = stockRepository;
        _storeRepository = storeRepository;
    }

    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IStoreRepository _storeRepository;

    public IEnumerable<Product> GetProducts()
    {
        return _productRepository.GetAll()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Stocks);
    }

    public IEnumerable<Product> GetProductsWhere(Expression<Func<Product, bool>> where)
    {
        return _productRepository.Get(where)
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Stocks);
    }

    public Product GetProductById(int id)
    {
        return _productRepository.Get(p => p.ProductId == id)
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Stocks)
            .First();
    }

    public bool AddProduct(Product product)
    {
        if (!_productRepository.Add(product))
            return false;

        return true;
    }

    public bool DeleteProduct(Product product)
    {
        if (!_productRepository.Delete(product))
            return false;

        return true;
    }

    public bool UpdateProduct(Product product)
    {
        if (!_productRepository.Update(product))
            return false;

        return true;
    }

    public IEnumerable<Stock> GetStocks()
    {
        return _stockRepository.GetAll()
            .Include(s => s.Product)
            .Include(s => s.Store);
    }

    public bool UpdateStock(Stock stock)
    {
        if (!_stockRepository.Update(stock))
            return false;

        return true;
    }

    public bool UpdateStock(int productId, int storeId, int quantity)
    {
        if (!_productRepository.IdExists(productId))
            return false;

        if (!_storeRepository.IdExists(storeId))
            return false;

        var stocks = _stockRepository.Get(s => s.ProductId == productId && s.StoreId == storeId);
        if (!stocks.Any())
        {
            var newStock = new Stock
            {
                ProductId = productId,
                StoreId = storeId,
                Quantity = 0
            };

            if (!_stockRepository.Add(newStock))
                return false;

            stocks = _stockRepository.Get(s => s.ProductId == productId && s.StoreId == storeId);
        }

        var stock = stocks.First();

        stock.Quantity = quantity;

        if (!UpdateStock(stock))
            return false;

        return true;
    }

    public IEnumerable<Brand> GetBrands()
    {
        return _brandRepository.GetAll()
            .Include(b => b.Products);
    }

    public IEnumerable<Brand> GetBrandsWhere(Expression<Func<Brand, bool>> where)
    {
        return _brandRepository.Get(where)
            .Include(b => b.Products);
    }

    public Brand GetBrandById(int id)
    {
        return _brandRepository.Get(b => b.BrandId == id)
            .Include(b => b.Products).First();
    }

    public bool AddBrand(Brand brand)
    {
        if (!_brandRepository.Add(brand))
            return false;

        return true;
    }

    public bool DeleteBrand(Brand brand)
    {
        if (!_brandRepository.Delete(brand))
            return false;

        return true;
    }

    public bool UpdateBrand(Brand brand)
    {
        if (!_brandRepository.Update(brand))
            return false;

        return true;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _categoryRepository.GetAll()
            .Include(c => c.Products);
    }

    public IEnumerable<Category> GetCategoriesWhere(Expression<Func<Category, bool>> where)
    {
        return _categoryRepository.Get(where)
            .Include(c => c.Products);
    }

    public Category GetCategoryById(int id)
    {
        return _categoryRepository.Get(c => c.CategoryId == id)
            .Include(c => c.Products).First();
    }

    public bool AddCategory(Category category)
    {
        if (!_categoryRepository.Add(category))
            return false;

        return true;
    }

    public bool DeleteCategory(Category category)
    {
        if (!_categoryRepository.Delete(category))
            return false;

        return true;
    }

    public bool UpdateCategory(Category category)
    {
        if (!_categoryRepository.Update(category))
            return false;

        return true;
    }
}

public interface IProductionService
{
    IEnumerable<Product> GetProducts();
    IEnumerable<Product> GetProductsWhere(Expression<Func<Product, bool>> where);
    Product GetProductById(int id);
    bool AddProduct(Product product);
    bool DeleteProduct(Product product);
    bool UpdateProduct(Product product);
    IEnumerable<Stock> GetStocks();
    bool UpdateStock(Stock stock);
    bool UpdateStock(int productId, int storeId, int quantity);
    IEnumerable<Brand> GetBrands();
    IEnumerable<Brand> GetBrandsWhere(Expression<Func<Brand, bool>> where);
    Brand GetBrandById(int id);
    bool AddBrand(Brand brand);
    bool DeleteBrand(Brand brand);
    bool UpdateBrand(Brand brand);
    IEnumerable<Category> GetCategories();
    IEnumerable<Category> GetCategoriesWhere(Expression<Func<Category, bool>> where);
    Category GetCategoryById(int id);
    bool AddCategory(Category category);
    bool DeleteCategory(Category category);
    bool UpdateCategory(Category category);
}
