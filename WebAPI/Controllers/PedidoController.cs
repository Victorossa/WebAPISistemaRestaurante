using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PedidoController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Pedido
        public IQueryable<Pedido> GetPedido()
        {
            return db.Pedido;
        }

        // GET: api/Pedido/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult GetPedido(long id)
        {
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        // PUT: api/Pedido/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedido(long id, Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedido.PedidoID)
            {
                return BadRequest();
            }

            db.Entry(pedido).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pedido
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult PostPedido(Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pedido.Add(pedido);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pedido.PedidoID }, pedido);
        }

        // DELETE: api/Pedido/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult DeletePedido(long id)
        {
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            db.Pedido.Remove(pedido);
            db.SaveChanges();

            return Ok(pedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoExists(long id)
        {
            return db.Pedido.Count(e => e.PedidoID == id) > 0;
        }
    }
}