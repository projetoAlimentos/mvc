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

  public class AttemptController : Controller
  {
    private readonly ApplicationDbContext _context;

    public AttemptController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Attempt
    public async Task<IActionResult> Index()
    {
      var applicationDbContext = _context.Attempt.Include(t => t.topico);
      return View(await applicationDbContext.ToListAsync());
    }

    // GET: Attempt/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var attempt = await _context.Attempt
          .Include(t => t.topico)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (attempt == null)
      {
        return NotFound();
      }

      return View(attempt);
    }

    // GET: Attempt/Create
    public IActionResult Create()
    {
      ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id");
      return View();
    }

    // POST: Attempt/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,TopicId")] Attempt attempt)
    {
      if (ModelState.IsValid)
      {
        _context.Add(attempt);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", attempt.TopicId);
      return View(attempt);
    }

    // GET: Attempt/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var attempt = await _context.Attempt.SingleOrDefaultAsync(m => m.Id == id);
      if (attempt == null)
      {
        return NotFound();
      }
      ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", attempt.TopicId);
      return View(attempt);
    }

    // POST: Attempt/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,TopicId")] Attempt attempt)
    {
      if (id != attempt.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(attempt);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!AttemptExists(attempt.Id))
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
      ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", attempt.TopicId);
      return View(attempt);
    }

    // GET: Attempt/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var attempt = await _context.Attempt
          .Include(t => t.topico)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (attempt == null)
      {
        return NotFound();
      }

      return View(attempt);
    }

    // POST: Attempt/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var attempt = await _context.Attempt.SingleOrDefaultAsync(m => m.Id == id);
      _context.Attempt.Remove(attempt);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool AttemptExists(int id)
    {
      return _context.Attempt.Any(e => e.Id == id);
    }
  }
}
