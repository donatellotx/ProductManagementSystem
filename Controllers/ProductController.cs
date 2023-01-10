using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Models;
using ProductManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private IproductRepository proRepo;
        private IFileUpload fileUpload;
        public ProductController(IproductRepository proRepo)
        {
            this.proRepo = proRepo;
            this.fileUpload = fileUpload;
        }

        // Get Request
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var pronew = new Product();
            pronew.Id = proRepo.GetMaxId();
            return View(pronew);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (await fileUpload.UploadFile(file))
                {
                    obj.Image = fileUpload.FileName;
                    proRepo.AddProduct(obj);
                    return RedirectToRoute(new { Action = "DisplayAll", Controller = "Product" });
                }
                else
                {
                    ViewBag.ErrorMessage = "File Upload failed";
                    return View(obj);
                }
            }
            ViewBag.Message = "Error adding product";
            return View(obj);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult DisplayAll()
        {
            AllProductsView model = new AllProductsView();
            model.Products = proRepo.GetProducts();
            return View(model);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Details(int? id)
        {
            var pro = proRepo.GetProduct(id);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }

        // Get Request
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var pro = proRepo.GetProduct(id);
            return View(pro);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            
 
            if (ModelState.IsValid)
            {
                proRepo.UpdateProduct(obj);
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Error editing product";
            return View(obj);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            proRepo.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
