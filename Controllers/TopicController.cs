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
  [Authorize]
  public class TopicController : Controller
  {
    private readonly ApplicationDbContext _context;

    public TopicController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Topic
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic")]
    public async Task<IActionResult> Index(int? id)
    {
      if (id == null)
      {
        var applicationDbContext = _context.Topic.Include(t => t.Module);
        return View(await applicationDbContext.ToListAsync());
      }
      else
      {
        var applicationDbContext = _context.Topic.
            Include(t => t.Module).
            Where(t => t.ModuleId == id);
        return View(await applicationDbContext.ToListAsync());
      }
    }

    // GET: Topic/Details/5
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/Details/{TopicId}")]
    public async Task<IActionResult> Details(int? TopicId)
    {
      if (TopicId == null)
      {
        return NotFound();
      }

      var topic = await _context.Topic
          .Include(t => t.Module)
          .SingleOrDefaultAsync(m => m.Id == TopicId);
      if (topic == null)
      {
        return NotFound();
      }

      return View(topic);
    }

    // GET: Topic/Create
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/Details/{TopicId}")]
    public IActionResult Create()
    {
      ViewData["ModuleId"] = new SelectList(_context.Module, "Id", "Id");
      return View();
    }

    // POST: Topic/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int? ModuleId, [Bind("Id,Name,Description,ModuleId,Active,Difficulty")] Topic topic)
    {
      if (ModelState.IsValid)
      {
        topic.ModuleId = ModuleId;
        _context.Add(topic);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["ModuleId"] = new SelectList(_context.Module, "Id", "Id", topic.ModuleId);
      return View(topic);
    }

    // GET: Topic/Edit/5
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/Edit/{TopicId}")]
    public async Task<IActionResult> Edit(int? TopicId)
    {
      if (TopicId == null)
      {
        return NotFound();
      }

      var topic = await _context.Topic.SingleOrDefaultAsync(m => m.Id == TopicId);
      if (topic == null)
      {
        return NotFound();
      }
      ViewData["ModuleId"] = new SelectList(_context.Module, "Id", "Id", topic.ModuleId);
      return View(topic);
    }

    // POST: Topic/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/Edit/{TopicId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int TopicId, [Bind("Id,Name,Description,ModuleId,Active,Difficulty")] Topic topic)
    {
      if (TopicId != topic.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(topic);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!TopicExists(topic.Id))
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
      ViewData["ModuleId"] = new SelectList(_context.Module, "Id", "Id", topic.ModuleId);
      return View(topic);
    }

    // GET: Topic/Delete/5
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/Delete/{TopicId}")]
    public async Task<IActionResult> Delete(int? TopicId)
    {
      if (TopicId == null)
      {
        return NotFound();
      }

      var topic = await _context.Topic
          .Include(t => t.Module)
          .SingleOrDefaultAsync(m => m.Id == TopicId);
      if (topic == null)
      {
        return NotFound();
      }

      return View(topic);
    }

    // POST: Topic/Delete/5
    [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/Delete/{TopicId}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int TopicId)
    {
      var topic = await _context.Topic.SingleOrDefaultAsync(m => m.Id == TopicId);
      _context.Topic.Remove(topic);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool TopicExists(int TopicId)
    {
      return _context.Topic.Any(e => e.Id == TopicId);
    }
  }
}
