using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RRental.Web.Models;

namespace RRental.Web.Controllers
{
    public class InventoryController : Controller
    {
        //TODO: add admin panel
        // GET: Inventory
        public ActionResult Index()
        {
            IEnumerable<Inventory> itemList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Inventories").Result;
            itemList = response.Content.ReadAsAsync<IEnumerable<Inventory>>().Result;
            return View(itemList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Inventory());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Inventories/" + id).Result;
                return View(response.Content.ReadAsAsync<Inventory>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(Inventory item)
        {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Inventories/" + item.Id, item).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Inventories/" + id).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}