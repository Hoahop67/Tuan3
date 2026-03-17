using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace Tuan3.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index(string searchString)
        {
            var data = from p in Data.SeedData.Products
                       join c in Data.SeedData.Categories on p.CategoryId equals c.CategoryId
                       select new Models.ProductViewModel
                       {
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           Price = p.Price,
                           CategoryName = c.CategoryName
                       };

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                // lọc theo tên sản phẩm hoặc tên danh mục, không phân biệt hoa thường
                data = data.Where(x =>
                    (!string.IsNullOrEmpty(x.ProductName) && x.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    || (!string.IsNullOrEmpty(x.CategoryName) && x.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                );
            }

            ViewData["CurrentFilter"] = searchString;
            return View(data.ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = Tuan3.Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);//tim san pham theo id
            var category = Tuan3.Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product?.CategoryId);
            var data = new Models.ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryName = category.CategoryName
            };
            return View(data);

        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Tuan3.Data.SeedData.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Product newProduct)
        {
            if (ModelState.IsValid)
            {
                newProduct.ProductId = Tuan3.Data.SeedData.Products.Max(p => p.ProductId) + 1;
                //them san pham moi vao danh sach
                Data.SeedData.Products.Add(newProduct);
                return RedirectToAction(nameof(Index));
            }
            //ViewBag.CategoryId = new SelectList(Bai5.Data.SeedData.GetCategories, "CategoryId", "CategoryName", newProduct.CategoryId);
            return View(newProduct);
        }

        // GET: ProductController/Edit/5
        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);//Tim san pham can chinh sua theo id
            if (product == null)//Neu khong tim thay san pham, tra ve trang loi 404 Not Found
            {
                return NotFound();
            }
            //Neu tim thay san pham, hien thi form chinh sua san pham va truyen danh sach danh muc de nguoi dung chon lai neu muon thay doi danh muc cua san pham
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }


        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Product EditProduct)
{
    if (ModelState.IsValid)
    {
        
        var existingProduct = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == EditProduct.ProductId);
        
        if (existingProduct != null)
        {
            existingProduct.ProductName = EditProduct.ProductName;
            existingProduct.Price = EditProduct.Price;
            existingProduct.CategoryId = EditProduct.CategoryId;
        }
        return RedirectToAction(nameof(Index));
    }
    //Neu du lieu khong hop le, hien thi lai form chinh sua san pham va truyen lai danh sach danh muc de nguoi dung chon lai neu muon thay doi danh muc cua san pham
    ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", EditProduct.CategoryId);
    return View();
    
}


        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}