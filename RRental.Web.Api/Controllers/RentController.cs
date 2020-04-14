using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using RRental.Web.Api.Data;
using RRental.Web.Api.Entities;
using RRental.Web.Api.Models;

namespace RRental.Web.Api.Controllers
{
    public class RentController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET inventory list
        public IQueryable<Inventory> GetInventories()
        {
            return db.Inventory;
        }

        [HttpPost]
        [ResponseType(typeof(Order))]
        public IHttpActionResult AddToCart(List<string> rows)
        {

            var cartNo = DateTime.Now.Ticks.ToString();
            var dateCreated = DateTime.Now;
            foreach (var row in rows)
            {
                var record = row.Split(';');
                if (record[2] != "0" && record[2] != String.Empty)
                {
                    
                    var tempName = record[0];
                    var orderModel = new Order {Id = Guid.NewGuid(),CartNo = cartNo, DateCreated = dateCreated};
                    Guid id = db.Inventory.Where(y => y.Name == tempName).Select(y => y.Id)
                        .SingleOrDefault();
                    orderModel.ItemId = id;
                    orderModel.Duration = Int32.Parse(record[2]);
                    db.Order.Add(orderModel);
                }
            }
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/Rent/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(Guid id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(Guid id)
        {
            return db.Order.Count(e => e.Id == id) > 0;
        }
    }
}