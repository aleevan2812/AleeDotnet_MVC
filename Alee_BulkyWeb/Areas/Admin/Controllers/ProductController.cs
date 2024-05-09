using AleeBook.DataAccess.Repository.IRepository;
using AleeBook.Models;
using AleeBook.Models.ViewModels;
using AleeBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AleeBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

        return View(objProductList);
    }

    //public IActionResult Create()
    //{
    //    //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
    //    //{
    //    //    Text = u.Name,
    //    //    Value = u.Id.ToString()
    //    //});

    //    //ViewBag.CategoryList = CategoryList;
    //    //ViewData["CategoryList"] = CategoryList;
    //    ProductVM productVM = new ProductVM()
    //    {
    //        CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
    //        {
    //            Text = u.Name,
    //            Value = u.Id.ToString()
    //        }),
    //        Product = new Product()
    //    };
    //    return View(productVM);
    //}

    public IActionResult Upsert(int? id) // Upsert: Update - Insert
    {
        var productVM = new ProductVM
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            Product = new Product()
        };

        if (id == null || id == 0)
            // Create
            return View(productVM);

        // Update
        productVM.Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties:"ProductImages");
        return View(productVM);
    }

    //[HttpPost]
    //public IActionResult Create(ProductVM productVM)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _unitOfWork.Product.Add(productVM.Product);
    //        _unitOfWork.Save();

    //        TempData["success"] = "Product created succesfully!";

    //        return RedirectToAction("Index");
    //    }
    //    else
    //    {
    //        productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
    //        {
    //            Text = u.Name,
    //            Value = u.Id.ToString()
    //        });
    //        return View(productVM);
    //    }
    //    //return View();
    //}

    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, List<IFormFile> files)
    {
        if (ModelState.IsValid)
        {
            // check for update or add
            if (productVM.Product.Id == 0)
                _unitOfWork.Product.Add(productVM.Product);
            else
                _unitOfWork.Product.Update(productVM.Product);

            _unitOfWork.Save();

            var wwwRootPath = _webHostEnvironment.WebRootPath;
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var productPath = @"images\products\product-" + productVM.Product.Id;
                    var finalPath = Path.Combine(wwwRootPath, productPath);

                    if (!Directory.Exists(finalPath))
                        Directory.CreateDirectory(finalPath);

                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    ProductImage productImage = new()
                    {
                        ImageUrl = @"\" + productPath + @"\" + fileName,
                        ProductId = productVM.Product.Id
                    };

                    if (productVM.Product.ProductImages == null)
                        productVM.Product.ProductImages = new List<ProductImage>();

                    productVM.Product.ProductImages.Add(productImage);

                    // _unitOfWork.ProductImage.Add(productImage);
                }
                
                _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Save();
            }

            // if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
            // {
            //     // delete the old image
            //     var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
            //
            //     if (System.IO.File.Exists(oldImagePath))
            //     {
            //         System.IO.File.Delete(oldImagePath);
            //     }
            // }
            //
            // using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
            // {
            //     file.CopyTo(fileStream);
            // }
            //
            // productVM.Product.ImageUrl = @"\images\product\" + fileName;
            
            TempData["success"] = "Product created/updated succesfully!";
            return RedirectToAction("Index");
        }

        productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        return View(productVM);
        //return View();
    }

    //public IActionResult Edit(int? id)
    //{
    //    if (id == null || id == 0)
    //        return NotFound();
    //    Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
    //    if (productFromDb == null)
    //        return NotFound();
    //    return View(productFromDb);
    //}

    //[HttpPost]
    //public IActionResult Edit(Product obj)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _unitOfWork.Product.Update(obj);
    //        _unitOfWork.Save();

    //        TempData["success"] = "Product updated succesfully!";

    //        return RedirectToAction("Index");
    //    }
    //    return View();
    //}

    //public IActionResult Delete(int? id)
    //{
    //    if (id == null || id == 0)
    //        return NotFound();
    //    Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
    //    if (productFromDb == null)
    //        return NotFound();
    //    return View(productFromDb);
    //}

    //[HttpPost, ActionName("Delete")]
    //public IActionResult DeletePOST(int? id)
    //{
    //    Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
    //    if (obj == null)
    //        return NotFound();
    //    _unitOfWork.Product.Remove(obj);
    //    _unitOfWork.Save();

    //    TempData["success"] = "Product deleted succesfully!";

    //    return RedirectToAction("Index");
    //}

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = objProductList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
        if (productToBeDeleted == null)
            return Json(new { success = false, massage = "Error while deleting" });

        // var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
        // if (System.IO.File.Exists(oldImagePath))
        // {
        //     System.IO.File.Delete(oldImagePath);
        // }
        _unitOfWork.Product.Remove(productToBeDeleted);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete Successful" });
    }

    #endregion API CALLS
}