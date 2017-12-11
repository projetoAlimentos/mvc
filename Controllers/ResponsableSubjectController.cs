using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projeto.Data;
using projeto.Models;

namespace projeto.Controllers
{
  [Authorize(Roles = "Administrador")]
  public class ResponsableSubjectController : Controller
  {
    private readonly ApplicationDbContext _context;

    public ResponsableSubjectController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: ResponsableSubject
    public async Task<IActionResult> Index()
    {
      var applicationDbContext = _context.ResponsableSubject.Include(r => r.Subject);
      return View(await applicationDbContext.ToListAsync());
    }

    // GET: ResponsableSubject/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var responsableSubject = await _context.ResponsableSubject
          .Include(r => r.Subject)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (responsableSubject == null)
      {
        return NotFound();
      }

      return View(responsableSubject);
    }

    // GET: ResponsableSubject/Create
    public IActionResult Create()
    {
      ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id");
      return View();
    }

    // POST: ResponsableSubject/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UserId,SubjectId")] ResponsableSubject responsableSubject)
    {
      if (ModelState.IsValid)
      {
        _context.Add(responsableSubject);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", responsableSubject.SubjectId);
      return View(responsableSubject);
    }

    // GET: ResponsableSubject/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var responsableSubject = await _context.ResponsableSubject.SingleOrDefaultAsync(m => m.Id == id);
      if (responsableSubject == null)
      {
        return NotFound();
      }
      ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", responsableSubject.SubjectId);
      return View(responsableSubject);
    }

    // POST: ResponsableSubject/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,SubjectId")] ResponsableSubject responsableSubject)
    {
      if (id != responsableSubject.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(responsableSubject);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ResponsableSubjectExists(responsableSubject.Id))
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
      ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", responsableSubject.SubjectId);
      return View(responsableSubject);
    }

    // GET: ResponsableSubject/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var responsableSubject = await _context.ResponsableSubject
          .Include(r => r.Subject)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (responsableSubject == null)
      {
        return NotFound();
      }

      return View(responsableSubject);
    }

    // POST: ResponsableSubject/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var responsableSubject = await _context.ResponsableSubject.SingleOrDefaultAsync(m => m.Id == id);
      _context.ResponsableSubject.Remove(responsableSubject);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ResponsableSubjectExists(int id)
    {
      return _context.ResponsableSubject.Any(e => e.Id == id);
    }
  }
}
