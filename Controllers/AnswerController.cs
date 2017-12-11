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
  [Authorize(Roles = "Admin")]
  public class AnswerController : Controller
  {
    private readonly ApplicationDbContext _context;

    public AnswerController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Answer
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}/Answer")]
    [Authorize(Roles = "Administrador,Professor,Assistente")]
    public async Task<IActionResult> Index(int? AnswerId)
    {
      if (AnswerId == null)
      {
        var applicationDbContext = _context.Answer.Include(a => a.Question);
        return View(await applicationDbContext.ToListAsync());
      }
      else
      {
        var applicationDbContext = _context.Answer.Include(a => a.Question).
            Where(a => a.QuestionId == AnswerId);
        return View(await applicationDbContext.ToListAsync());
      }
    }

    // GET: Answer/Details/5
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}/Answer/{AnswerId}")]
    [Authorize(Roles = "Administrador,Professor,Assistente")]
    public async Task<IActionResult> Details(int? AnswerId)
    {
      if (AnswerId == null)
      {
        return NotFound();
      }

      var answer = await _context.Answer
          .Include(a => a.Question)
          .SingleOrDefaultAsync(m => m.Id == AnswerId);
      if (answer == null)
      {
        return NotFound();
      }

      return View(answer);
    }

    // GET: Answer/Create
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}/Answer/Create")]
    [Authorize(Roles = "Administrador,Professor,Assistente")]
    public IActionResult Create()
    {
      ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id");
      return View();
    }

    // POST: Answer/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}/Answer/Create")]
    [Authorize(Roles = "Administrador,Professor,Assistente")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int? QuestionId,[Bind("Id,Description,Correct,QuestionId")] Answer answer)
    {
      if (ModelState.IsValid)
      {
        answer.QuestionId = QuestionId;
        _context.Add(answer);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answer.QuestionId);
      return View(answer);
    }

    // GET: Answer/Edit/5
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}/Answer/Edit/{AnswerId}")]
    [Authorize(Roles = "Administrador,Professor,Assistente")]
    public async Task<IActionResult> Edit(int? AnswerId)
    {
      if (AnswerId == null)
      {
        return NotFound();
      }

      var answer = await _context.Answer.SingleOrDefaultAsync(m => m.Id == AnswerId);
      if (answer == null)
      {
        return NotFound();
      }
      ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answer.QuestionId);
      return View(answer);
    }

    // POST: Answer/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}/Answer/Edit/{AnswerId}")]
    [Authorize(Roles = "Administrador,Professor,Assistente")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int AnswerId, [Bind("Id,Description,Correct,QuestionId")] Answer answer)
    {
      if (AnswerId != answer.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(answer);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!AnswerExists(answer.Id))
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
      ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "Id", answer.QuestionId);
      return View(answer);
    }

    // GET: Answer/Delete/5
    [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}/Answer/Delete/{AnswerId}")]
    [Authorize(Roles = "Administrador,Professor")]
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var answer = await _context.Answer
          .Include(a => a.Question)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (answer == null)
      {
        return NotFound();
      }

      return View(answer);
    }

    // POST: Answer/Delete/5
    [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}/Answer/Delete/{AnswerId}"), 
        ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,Professor")]
    public async Task<IActionResult> DeleteConfirmed(int AnswerId)
    {
      var answer = await _context.Answer.SingleOrDefaultAsync(m => m.Id == AnswerId);
      _context.Answer.Remove(answer);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool AnswerExists(int id)
    {
      return _context.Answer.Any(e => e.Id == id);
    }
  }
}
