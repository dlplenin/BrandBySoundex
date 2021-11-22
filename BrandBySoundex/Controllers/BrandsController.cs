//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrandBySoundex.Data;
using BrandBySoundex.Models;

namespace BrandBySoundex.Controllers
{
    public class BrandsController : Controller
    {
        private readonly BrandBySoundexContext _context;

        public BrandsController(BrandBySoundexContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index(string searchString, int drop_strict_comparision)
        {
            var existsSearchString = false;
            var brands = from m in _context.Brand
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                existsSearchString = true;
                //movies = movies.Where(s => s.Marca!.Contains(searchString));
                //brands = brands
                //    .Where(e => BrandBySoundexContext.Soundex(e.Marca) == BrandBySoundexContext.Soundex(searchString));

                brands = brands
                    .Where(e => BrandBySoundexContext
                    .Difference(e.Marca, searchString) >= drop_strict_comparision)
                    .Select(brand => new Brand
                    {
                        Id = brand.Id,
                        Marca = brand.Marca,
                        DifferenceSoundex = BrandBySoundexContext.Difference(brand.Marca, searchString)
                    });
            }

            ViewData["ExistsSearchString"] = existsSearchString;
            return View(await brands.ToListAsync());
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marca")] Brand brand)
        {
            if (BrandExistsByName(brand.Marca))
            {
                return Conflict("La marca ya existe.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marca")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _context.Brand.FindAsync(id);
            _context.Brand.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _context.Brand.Any(e => e.Id == id);
        }

        private bool BrandExistsByName(string brand)
        {
            return _context.Brand.Any(e => e.Marca == brand);
        }
    }
}
