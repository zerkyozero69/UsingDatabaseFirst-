using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UsingDatabaseFirst.Models.db;
using UsingDatabaseFirst.ViewModels;

namespace UsingDatabaseFirst.Controllers
{
    public class ProductController : Controller
    {
        private readonly thaivbShopContext _db;

        public ProductController(thaivbShopContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
             var pcs = (from p in _db.Product
                       from c in _db.Category
                       where (p.CategoryId == c.CategoryId)                       
                       select new ProductCategoryViewModel
                       {
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           ProductPrice = p.ProductPrice,
                           CategoryName = c.CategoryName
                       }).OrderByDescending(i => i.ProductPrice);

            if (pcs == null)
            {
                return NotFound();
            }

            return View(await pcs.ToListAsync());
        }
        
        public async Task<IActionResult> Search(string txtProductName)
        {
            if (string.IsNullOrEmpty(txtProductName))
            {
                var pcs = (from p in _db.Product
                           from c in _db.Category
                           where (p.CategoryId == c.CategoryId)
                           select new ProductCategoryViewModel
                           {
                               ProductId = p.ProductId,
                               ProductName = p.ProductName,
                               ProductPrice = p.ProductPrice,
                               CategoryName = c.CategoryName
                           }).OrderByDescending(i => i.ProductPrice);

                if (pcs == null)
                {
                    return NotFound();
                }

                return View("Index", await pcs.ToListAsync());
            }
            else
            {
                var pcs = (from p in _db.Product
                           from c in _db.Category
                           where (p.CategoryId == c.CategoryId)
                           && (p.ProductName.Contains(txtProductName))
                           select new ProductCategoryViewModel
                           {
                               ProductId = p.ProductId,
                               ProductName = p.ProductName,
                               ProductPrice = p.ProductPrice,
                               CategoryName = c.CategoryName
                           }).OrderByDescending(i => i.ProductPrice);

                if (pcs == null)
                {
                    return NotFound();
                }

                return View("Index", await pcs.ToListAsync());
            }
        }    
        
        public async Task<IActionResult> Detail(int id)
        {
            var pds = await (from p in _db.Product
                      from c in _db.Category
                      from s in _db.Supplier
                      where (p.CategoryId == c.CategoryId)
                      && (p.SupplierId == s.SupplierId)
                      && (p.ProductId == id)
                      select new ProductDetailViewModel
                      {
                          ProductId=p.ProductId,
                          ProductName=p.ProductName,
                          ProductPrice=p.ProductPrice,
                          CategoryName=c.CategoryName,
                          SupplierName= s.SupplierName
                      }).FirstOrDefaultAsync();

            if (pds==null)
            {
                return NotFound();
            }

            return View(pds);
        }

        public IActionResult Create()
        {
            ViewData["CategoryLists"] = new SelectList(_db.Category, "CategoryId", "CategoryName");
            ViewData["SupplierLists"] = new SelectList(_db.Supplier, "SupplierId", "SupplierName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel data)
        {
            if (ModelState.IsValid)
            {
                Product p = new Product();
                p.ProductName = data.ProductName;
                p.ProductPrice = data.ProductPrice;
                p.CategoryId = data.CategoryId;
                p.SupplierId = data.SupplierId;
                p.ProductStatus = data.ProductStatus;

                p.ProductCost = 0;
                p.ProductTypeId = "1";
                p.SerialNumber = "";
                p.UnitId = "p1";
                p.ProductInStock = 0;
                p.ProductInStockWithUnit = 0;
                p.ProductInOrder = 0;
                p.ProductInOrderWithUnit = 0;                

                _db.Add(p);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryLists"] = new SelectList(_db.Category, "CategoryId", "CategoryName", data.CategoryId);
            ViewData["SupplierLists"] = new SelectList(_db.Supplier, "SupplierId", "CompanyName", data.SupplierId);
            return View(data);
        }
    }
}



