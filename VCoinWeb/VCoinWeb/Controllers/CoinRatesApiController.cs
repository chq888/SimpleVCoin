using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Domain.CoinRate;

namespace VCoinWeb.Controllers
{

    [RoutePrefix("api/CoinRates/v1")]
    public class CoinRatesApiController : ApiController
    {
        private VCoinDbContext db = new VCoinDbContext();

        // GET: api/CoinRatesApi
        [HttpGet]
        public IQueryable<CoinRate> Gets()
        {
            return db.CoinRates;
        }

        // GET: api/CoinRatesApi/5
        [Route("{id:int}")]
        //Use a tilde (~) on the method attribute to override the route prefix:
        [Route("~/api/CoinRates/v1/{id:int}")]
        [ResponseType(typeof(CoinRate))]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            CoinRate coinRate = await db.CoinRates.FindAsync(id);
            if (coinRate == null)
            {
                return NotFound();
            }

            return Ok(coinRate);
        }

        // PUT: api/CoinRatesApi/5
        [ResponseType(typeof(void))]
        [HttpPost, HttpPut, HttpPatch]
        public async Task<IHttpActionResult> Update(int id, CoinRate coinRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coinRate.Id)
            {
                return BadRequest();
            }

            db.Entry(coinRate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();

                FCMPushNotification fcm = new FCMPushNotification();
                fcm.SendNotification(coinRate.Link.Name, coinRate.Rate, coinRate.LinkId, "LatestRate");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinRateExists(id))
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

        // POST: api/CoinRatesApi
        [ResponseType(typeof(CoinRate))]
        [HttpPost]
        public async Task<IHttpActionResult> Add(CoinRate coinRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CoinRates.Add(coinRate);
            await db.SaveChangesAsync();


            FCMPushNotification fcm = new FCMPushNotification();
            fcm.SendNotification(coinRate.Link.Name, coinRate.Rate, coinRate.LinkId, "LatestRate");

            return CreatedAtRoute("DefaultApi", new { id = coinRate.Id }, coinRate);
        }

        // DELETE: api/CoinRatesApi/5
        [ResponseType(typeof(CoinRate))]
        [HttpPost, HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            CoinRate coinRate = await db.CoinRates.FindAsync(id);
            if (coinRate == null)
            {
                return NotFound();
            }

            db.CoinRates.Remove(coinRate);
            await db.SaveChangesAsync();

            return Ok(coinRate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoinRateExists(int id)
        {
            return db.CoinRates.Count(e => e.Id == id) > 0;
        }
    }
}