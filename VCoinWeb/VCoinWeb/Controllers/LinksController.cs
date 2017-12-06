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
    public class LinksController : Controller
    {
        private VCoinDbContext db = new VCoinDbContext();

        // GET: Links
        public async Task<ActionResult> Index()
        {
            return View(await db.Links.ToListAsync());
        }

        // GET: Links/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Links/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Note,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsActived,IsDeleted")] Link link)
        {
            if (ModelState.IsValid)
            {
                //dbContext.Links.Add(new Link()
                //{
                //    Name = "Link 1",
                //    Note = "note 1",
                //    CreatedBy = "",
                //    CreatedDate = DateTime.Now,
                //    UpdatedBy = "",
                //    UpdatedDate = DateTime.Now,
                //    IsActived = true,
                //    IsDeleted = false
                //});

                //    Name = "Link 1",
                //    Note = "note 1",
                link.CreatedBy = "Auto";
                link.CreatedDate = DateTime.Now;
                link.UpdatedBy = "Auto";
                link.UpdatedDate = DateTime.Now;
                link.IsActived = true;
                link.IsDeleted = false;
                db.Links.Add(link);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Note,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsActived,IsDeleted")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.CreatedBy = "Auto";
                link.CreatedDate = DateTime.Now;
                link.UpdatedBy = "Auto";
                link.UpdatedDate = DateTime.Now;
                link.IsActived = true;
                link.IsDeleted = false;

                db.Entry(link).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Link link = await db.Links.FindAsync(id);
            db.Links.Remove(link);
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
