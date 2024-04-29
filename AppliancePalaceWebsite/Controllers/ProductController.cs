using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace AppliancePalaceWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            Product? product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAll();

            ViewBag.Categories = new SelectList(categories, "id", "name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductRequest request)
        {
            Product product= new()
            {
                Name = request.Name,
                Description = request.Description,
                Brand = request.Brand,
                Price = request.Price,
                Qunatity = request.Qunatity,
                CategoryId = request.CategoryId,
            };

            if (request.Image != null)
            {
                var uploadsDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "products");

                if (!Directory.Exists(uploadsDirectory))
                    Directory.CreateDirectory(uploadsDirectory);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                var filePath = Path.Combine(uploadsDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    request.Image.CopyTo(stream);
                }

                product.ImagePath = Path.Combine("products", fileName);
            }

            await _productRepository.Add(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Product? product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            IEnumerable<Category> categories = await _categoryRepository.GetAll();

            ViewBag.Categories = new SelectList(categories, "id", "name");

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductRequestForUpdate request)
        {
            Product product = new()
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Brand = request.Brand,
                Price = request.Price,
                Qunatity = request.Qunatity,
                CategoryId = request.CategoryId,
            };

            if (request.Image != null)
            {
                var uploadsDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "products");

                if (!Directory.Exists(uploadsDirectory))
                    Directory.CreateDirectory(uploadsDirectory);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                var filePath = Path.Combine(uploadsDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    request.Image.CopyTo(stream);
                }

                product.ImagePath = Path.Combine("products", fileName);
            }
            try
            {
                await _productRepository.Edit(product);
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }


            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Delete/5
/*        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }*/

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productRepository.Remove(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string? name, string? brand, int categoryId)

        {
            var result = await _productRepository.Filter(name, brand, categoryId);

            return View("Index", result);
        }
    }
}
