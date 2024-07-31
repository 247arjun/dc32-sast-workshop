using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VulnerableWebApp.Framework;
using VulnerableWebApp.Models;

namespace namespace CSharpChallenges
{
    public class NoAuthorizeController : Controller
    {
        [Authorize("Admin")]
        public ActionResult Index(int productId)
        {
            var productsRepo = new ProductsRepository();
            var productInfo = productsRepo.GetProductInfo(productId);
            return View(productInfo);
        }

        [Authorize("Admin")]
        public ActionResult AddCategory()
        {
            List<Categories> categoriesList = new List<Categories>();
            categoriesList = GetCategories();
            return View(categoriesList);
        }

        [Authorize("Admin")]
        [HttpPost]
        public List<Categories> GetCategories()
        {
            List<Categories> categoriesList = new List<Categories>();
            var productsRepo = new ProductsRepository();
            categoriesList = productsRepo.GetCategories();
            return categoriesList;
        }

        [Authorize("Admin")]
        [HttpPost]
        public ActionResult AddCategory(Categories categoriesModel)
        {
            var productsRepo = new ProductsRepository();
            productsRepo.AddCategory(categoriesModel);
            ViewBag.Message = "Category Added successfully!";
            return View(GetCategories());
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductsModel productsModel)
        {
            var productsRepo = new ProductsRepository();
            productsRepo.AddProduct(productsModel);
            ViewBag.Message = "Product Added successfully!";
            return View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public ActionResult AddProductRating(ProductRatings productRatingsModel)
        {
            var productsRepo = new ProductsRepository();
            productsRepo.AddProductRating(productRatingsModel);
            ViewBag.Message = "Rating Added successfully!";
            var productInfo = productsRepo.GetProductInfo(productRatingsModel.ProductId);
            return RedirectToAction("Index", "Product", productInfo);
        }
        public ActionResult EditProduct(int productId)
        {
            var productsRepo = new ProductsRepository();
            var productsModel = productsRepo.GetProductInfo(productId);
            return View(productsModel);
        }
        [HttpPost]
        public ActionResult EditProduct(ProductsModel productsModel)
        {
            var productId = productsModel.ProductId;
            var productsRepo = new ProductsRepository();
            productsRepo.EditProduct(productsModel);
            var productsModelNew = productsRepo.GetProductInfo(productId);
            ViewBag.Message = "Product Updated successfully!";
            return View(productsModelNew);
        }
    }
}
