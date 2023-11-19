using Assignment.Entities;
using Assignment.Models;
using Assignment.Repositories;
using System.Linq.Expressions;

namespace Assignment.Services;

public class ProductService
{
    //lagra repositories
    private readonly ProductRepository _productRepository;
    private readonly PricingUnitRepository _pricingUnitRepository;
    private readonly ProductCategoryRepository _productCategoryRepository;

   // konstruktor tar emot och initierar repositories
    public ProductService(ProductRepository productRepository, PricingUnitRepository pricingUnitRepository, ProductCategoryRepository productCategoryRepository)
    {
        _productRepository = productRepository;
        _pricingUnitRepository = pricingUnitRepository;
        _productCategoryRepository = productCategoryRepository;
    }

    // Skapar en ny produkt
    public async Task<bool> CreateProductAsync(ProductRegistrationForm form)
    {
        // Kontrollera om produkten redan finns
        if (!await _productRepository.ExistsAsync(x => x.ProductName == form.ProductName))
        {
            // Kontrollera om psrisUnit redan finns, annars skapa 
            var pricingUnitEntity = await _pricingUnitRepository.GetAsync(x => x.Unit == form.PricingUnit);
            pricingUnitEntity ??= await _pricingUnitRepository.CreateAsync(new PricingUnitEntity { Unit = form.PricingUnit });

            // Kontrollera om category finns annasr skapa 
            var productCategoryEntity = await _productCategoryRepository.GetAsync(x => x.CategoryName == form.ProductCategory);
            productCategoryEntity ??= await _productCategoryRepository.CreateAsync(new ProductCategoryEntity { CategoryName = form.ProductCategory });

            // Skapa produkt
            var productEntity = await _productRepository.CreateAsync(new ProductEntity
            {
                ProductName = form.ProductName,
                ProductDescription = form.ProductDescription,
                ProductPrice = form.ProductPrice,
                PricingUnitId = pricingUnitEntity.Id,
                ProductCategoryId = productCategoryEntity.Id
            });
            // Returnera true om produkten skapades 
            if (productEntity != null)
                return true;
        }
        // Returnera false om produkten redan finns
        return false;
    }

    // Hämtar alla produkter
    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products;
    }
    // Hämtar alla PrisUnits
    public async Task<IEnumerable<PricingUnitEntity>> GetAllPricingUnitsAsync()
    {
        var units = await _pricingUnitRepository.GetAllAsync();
        return units;
    }
    //HÄMTA EN produkt via prduktname
    public async Task<ProductEntity> GetAsync(string productName)
    {
        return await _productRepository.GetAsync(x => x.ProductName == productName);
    }
    //UPDATE

    public async Task<bool> UpdateProductAsync(ProductUpdateForm form, string productName)
    {
        // Hämta kund baserat på e-postadress
        var existingProduct = await _productRepository.GetAsync(x => x.ProductName == productName);

        // Om kunden inte finns, returnera false
        if (existingProduct == null || existingProduct.ProductName == null)
        {
            return false;
        }

        // Uppdatera produktens egenskaper med värden från formuläret
        existingProduct.ProductDescription = form.ProductDescription;
        existingProduct.ProductPrice = form.ProductPrice;
        existingProduct.PricingUnit.Unit = form.PricingUnit;
        existingProduct.ProductCategory.CategoryName = form.ProductCategory;
        


        // Uppdatera produkt i databasen
        var updatedProduct = await _productRepository.UpdateAsync(existingProduct);

        // Returnera true om uppdateringen lyckades, annars false
        return updatedProduct != null;
    }
    // DELETE

    // Metod för att ta bort en produkt baserat på namn
    public async Task<bool> DeleteAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        // Hämta produkt baserat på namn
        var product = await _productRepository.GetAsync(expression);

        // Om produkten finns, ta bort och spara ändringar
        if (product != null)
        {
            await _productRepository.DeleteAsync(expression);
            return true;
        }

        // Om produkt inte finns
        return false;
    }

    // Metod för att ta bort en kund baserat på productName
    public async Task<bool> DeleteAsync(string productName)
    {
        // Använd DeleteAsync från product Repository för att ta bort produkt baserat på produktnamn
        return await _productRepository.DeleteAsync(x => x.ProductName == productName);
    }
}

