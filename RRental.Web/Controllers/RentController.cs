using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RRental.Web.Models;

namespace RRental.Web.Controllers
{
    public class RentController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Inventory> itemList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Inventories").Result;
            itemList = response.Content.ReadAsAsync<IEnumerable<Inventory>>().Result;
            return View(itemList);
        }

        public IActionResult AddToCart(List<string> rows)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Rent", rows).Result;
            TempData["SuccessMessage"] = "Saved Successfully";

            return View("Index");
        }
    }
}