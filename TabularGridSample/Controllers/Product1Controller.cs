using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TabularGridSample.Models;

namespace TabularGridSample.Controllers
{
    public class Product1Controller : Controller
    {

        AdventureWorksLTEntities db = new AdventureWorksLTEntities();
        const int pageSize = 10;

        [HttpGet]
        public ActionResult Products(int page = 1, int sortBy = 1, bool isAsc = true, string search = null)
        {
            IEnumerable<Product> products = db.Products.Where(
                    p => search == null
                    || p.Name.Contains(search)
                    || p.ProductNumber.Contains(search)
                    || p.Color.Contains(search));

            #region Sorting
            switch (sortBy)
            {
                case 1:
                    products = isAsc ? products.OrderBy(p => p.ProductID) : products.OrderByDescending(p => p.ProductID);
                    break;

                case 2:
                    products = isAsc ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name);
                    break;

                case 3:
                    products = isAsc ? products.OrderBy(p => p.ProductNumber) : products.OrderByDescending(p => p.ProductNumber);
                    break;

                case 4:
                    products = isAsc ? products.OrderBy(p => p.Color) : products.OrderByDescending(p => p.Color);
                    break;

                case 5:
                    products = isAsc ? products.OrderBy(p => p.StandardCost) : products.OrderByDescending(p => p.StandardCost);
                    break;

                case 6:
                    products = isAsc ? products.OrderBy(p => p.ListPrice) : products.OrderByDescending(p => p.ListPrice);
                    break;

                case 7:
                    products = isAsc ? products.OrderBy(p => p.Size) : products.OrderByDescending(p => p.Size);
                    break;

                default:
                    products = isAsc ? products.OrderBy(p => p.Weight) : products.OrderByDescending(p => p.Weight);
                    break;
            }
            #endregion

            ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count() / pageSize);

            products = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            ViewBag.Search = search;

            ViewBag.SortBy = sortBy;
            ViewBag.IsAsc = isAsc;

            return View(products);
        }
    }
}