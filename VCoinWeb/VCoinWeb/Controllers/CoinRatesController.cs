using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.CoinRate;

namespace VCoinWeb.Controllers
{
    public class CoinRatesController : Controller
    {
        private VCoinDbContext db = new VCoinDbContext();

        // GET: CoinRates
        public async Task<ActionResult> Index()
        {
            var coinRates = db.CoinRates.Include(c => c.Link);
            return View(await coinRates.ToListAsync());
        }

        // GET: CoinRates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoinRate coinRate = await db.CoinRates.FindAsync(id);
            if (coinRate == null)
            {
                return HttpNotFound();
            }
            return View(coinRate);
        }

        // GET: CoinRates/Create
        public ActionResult Create()
        {
            ViewBag.LinkId = new SelectList(db.Links, "Id", "Name");
            return View();
        }

        // POST: CoinRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Rate,LinkId,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsActived,IsDeleted")] CoinRate coinRate)
        {
            if (ModelState.IsValid)
            {
                coinRate.CreatedBy = "Auto";
                coinRate.CreatedDate = DateTime.Now;
                coinRate.UpdatedBy = "Auto";
                coinRate.UpdatedDate = DateTime.Now;
                coinRate.IsActived = true;
                coinRate.IsDeleted = false;
                db.CoinRates.Add(coinRate);
                await db.SaveChangesAsync();

                var link = db.Links.Single(a => a.Id == coinRate.LinkId);
                FCMPushNotification fcm = new FCMPushNotification();
                fcm.SendNotification(link.Name, coinRate.Rate, coinRate.LinkId, "LatestRate");

                return RedirectToAction("Index");
            }

            ViewBag.LinkId = new SelectList(db.Links, "Id", "Name", coinRate.LinkId);
            return View(coinRate);
        }

        // GET: CoinRates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoinRate coinRate = await db.CoinRates.FindAsync(id);
            if (coinRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.LinkId = new SelectList(db.Links, "Id", "Name", coinRate.LinkId);
            return View(coinRate);
        }

        // POST: CoinRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Rate,LinkId,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsActived,IsDeleted")] CoinRate coinRate)
        {
            if (ModelState.IsValid)
            {
                coinRate.CreatedBy = "Auto";
                coinRate.CreatedDate = DateTime.Now;
                coinRate.UpdatedBy = "Auto";
                coinRate.UpdatedDate = DateTime.Now;
                coinRate.IsActived = true;
                coinRate.IsDeleted = false;
                db.Entry(coinRate).State = EntityState.Modified;
                await db.SaveChangesAsync();

                var link = db.Links.Single(a => a.Id == coinRate.LinkId);
                FCMPushNotification fcm = new FCMPushNotification();
                fcm.SendNotification(link.Name, coinRate.Rate, coinRate.LinkId, "LatestRate");

                return RedirectToAction("Index");
            }
            ViewBag.LinkId = new SelectList(db.Links, "Id", "Name", coinRate.LinkId);
            return View(coinRate);
        }

        // GET: CoinRates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoinRate coinRate = await db.CoinRates.FindAsync(id);
            if (coinRate == null)
            {
                return HttpNotFound();
            }
            return View(coinRate);
        }

        // POST: CoinRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CoinRate coinRate = await db.CoinRates.FindAsync(id);
            db.CoinRates.Remove(coinRate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
