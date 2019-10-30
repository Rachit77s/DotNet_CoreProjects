using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationDI.Data;
using ApplicationDI.Models;
using ApplicationDI.Services;

namespace ApplicationDI.Controllers
{
    public class ContactModelsController : Controller
    {
        private readonly IContact _Icontact;

        public ContactModelsController(IContact Icontact)
        {
            _Icontact = Icontact;
        }

        // GET: ContactModels
        public IActionResult Index()
        {
            var a = _Icontact.getAll();
            //return View(_context.contacts.ToListAsync());
            return View(a);
        }

        // GET: ContactModels/Details/5
        public IActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var contactModel = await _context.contacts
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (contactModel == null)
            //{
            //    return NotFound();
            //}

            ContactModel cont = _Icontact.getById(id);
            return View(cont);
        }

        // GET: ContactModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,Status")] ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                //    _context.Add(contactModel);
                //    await _context.SaveChangesAsync();
                //    return RedirectToAction(nameof(Index));
                //}
                _Icontact.Insert(contactModel);
                _Icontact.Save();
            }
            return RedirectToAction("Index");
        }

        // GET: ContactModels/Edit/5
        public IActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var contactModel = await _context.contacts.FindAsync(id);
            //if (contactModel == null)
            //{
            //    return NotFound();
            //}
            //return View(contactModel);

            ContactModel cont = _Icontact.getById(id);
            return View(cont);

        }

        // POST: ContactModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,Status")] ContactModel contactModel)
        {
        //    if (id != contactModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(contactModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ContactModelExists(contactModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contactModel);

                _Icontact.Update(contactModel);
                _Icontact.Save();
            return RedirectToAction("Index");

        }

        // GET: ContactModels/Delete/5
        public IActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var contactModel = await _context.contacts
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (contactModel == null)
            //{
            //    return NotFound();
            //}

            //return View(contactModel);


            _Icontact.Delete(id);
            _Icontact.Save();
            return RedirectToAction("Index");

        }

        // POST: ContactModels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var contactModel = await _context.contacts.FindAsync(id);
        //    _context.contacts.Remove(contactModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ContactModelExists(int id)
        //{
        //    return _context.contacts.Any(e => e.Id == id);
        //}
    }
}
