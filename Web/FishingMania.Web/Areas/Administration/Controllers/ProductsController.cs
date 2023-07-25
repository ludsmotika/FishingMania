namespace FishingMania.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FishingMania.Data;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    [Area("Administration")]
    public class ProductsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Manufacturer> manufacturersRepository;
        private readonly IDeletableEntityRepository<ProductCategory> productCategoriesRepository;
        private readonly IImageService imageService;

        public ProductsController(IImageService imageService, IDeletableEntityRepository<Product> productsRepository, IDeletableEntityRepository<Manufacturer> manufacturersRepository, IDeletableEntityRepository<ProductCategory> productCategoriesRepository)
        {
            this.productsRepository = productsRepository;
            this.manufacturersRepository = manufacturersRepository;
            this.productCategoriesRepository = productCategoriesRepository;
            this.imageService = imageService;
        }

        // GET: Administration/Products
        public async Task<IActionResult> Index(string searchQuery = "")
        {
            IIncludableQueryable<Product, ProductCategory> products;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                string wildCard = $"%{searchQuery.ToLower()}%";
                products = this.productsRepository.AllWithDeleted().Where(p => EF.Functions.Like(p.Description, wildCard)).Include(p => p.Manufacturer).Include(p => p.ProductCategory);
            }
            else
            {
                products = this.productsRepository.AllWithDeleted().Include(p => p.Manufacturer).Include(p => p.ProductCategory);
            }

            return this.View(await products.ToListAsync());
        }

        // GET: Administration/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.productsRepository == null)
            {
                return this.NotFound();
            }

            var product = await this.productsRepository.AllWithDeleted()
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        // GET: Administration/Products/Create
        public IActionResult Create()
        {
            this.ViewData["ManufacturerId"] = new SelectList(this.manufacturersRepository.All(), "Id", "Name");
            this.ViewData["CategoryId"] = new SelectList(this.productCategoriesRepository.All(), "Id", "Name");
            return this.View();
        }

        // POST: Administration/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel product)
        {
            List<Image> images = new List<Image>();

            foreach (var image in product.Images)
            {
                images.Add(await this.imageService.AddByFile(image, image.FileName));
            }

            if (this.ModelState.IsValid)
            {
                Product productToAdd = new Product()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    AmountInStock = product.AmountInStock,
                    ManufacturerId = product.ManufacturerId,
                    CategoryId = product.CategoryId,
                    Images = images,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                };

                await this.productsRepository.AddAsync(productToAdd);
                await this.productsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["ManufacturerId"] = new SelectList(this.manufacturersRepository.All(), "Id", "Name", product.ManufacturerId);
            this.ViewData["CategoryId"] = new SelectList(this.productCategoriesRepository.All(), "Id", "Name", product.CategoryId);
            return this.View(product);
        }

        // GET: Administration/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.productsRepository == null)
            {
                return this.NotFound();
            }

            var product = await this.productsRepository.AllWithDeleted().Include(p => p.Manufacturer).Include(p => p.ProductCategory).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return this.NotFound();
            }

            this.ViewData["ManufacturerId"] = new SelectList(this.manufacturersRepository.All(), "Id", "Name", product.ManufacturerId);
            this.ViewData["CategoryId"] = new SelectList(this.productCategoriesRepository.All(), "Id", "Name", product.CategoryId);
            return this.View(product);
        }

        // POST: Administration/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Price,Description,AmountInStock,ManufacturerId,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Product product)
        {
            if (id != product.Id)
            {
                return this.NotFound();
            }

            product.Manufacturer = await this.manufacturersRepository.AllWithDeleted().FirstOrDefaultAsync(m => m.Id == product.ManufacturerId);
            this.ModelState.ClearValidationState("Manufacturer");
            this.ModelState.MarkFieldValid("Manufacturer");

            product.ProductCategory = await this.productCategoriesRepository.AllWithDeleted().FirstOrDefaultAsync(m => m.Id == product.CategoryId);
            this.ModelState.ClearValidationState("ProductCategory");
            this.ModelState.MarkFieldValid("ProductCategory");

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.productsRepository.Update(product);
                    await this.productsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ProductExists(product.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["ManufacturerId"] = new SelectList(this.manufacturersRepository.All(), "Id", "Name", product.ManufacturerId);
            this.ViewData["CategoryId"] = new SelectList(this.productCategoriesRepository.All(), "Id", "Name", product.CategoryId);
            return this.View(product);
        }

        // GET: Administration/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.productsRepository == null)
            {
                return this.NotFound();
            }

            var product = await this.productsRepository.All()
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(product);
        }

        // POST: Administration/Products/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this.productsRepository == null)
            {
                return this.Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }

            var product = await this.productsRepository.All().FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                this.productsRepository.Delete(product);
            }

            await this.productsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ProductExists(int id)
        {
            return this.productsRepository.All().Any(e => e.Id == id);
        }
    }
}
