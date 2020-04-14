using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RRental.Web.Models;

namespace RRental.Web.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Order> itemList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Invoice").Result;
            itemList = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
            return View(itemList);
        }

        public async Task<ActionResult> Create(string cartNo)
        {
            var url = GlobalVariables.WebApiClient.BaseAddress;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url+"Invoice/getinvoice/" + cartNo).Result;
                TempData["SuccessMessage"] = "Request Successful";
                string path = @"C:\Temp\" + cartNo + ".txt";

                var stream = response.Content.ReadAsByteArrayAsync();

                System.IO.File.WriteAllBytes(path, await stream);

                var fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

                return File(fileStream, "application/text", cartNo+".txt");
            }
        }
    }
}