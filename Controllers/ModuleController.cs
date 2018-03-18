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
    public class ModuleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModuleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Module
        [HttpGet("/Subject/{SubjectId}/Module")]
        [Authorize(Roles = "Administrador,Professor,Assistente,Aluno")]
        public async Task<IActionResult> Index(int? SubjectId)
        {
            if (SubjectId == null)
            {
                var applicationDbContext = _context.Module.
                        Include(m => m.Subject);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Module.
                        Include(m => m.Subject).
                        Where(m => m.SubjectId == SubjectId);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Module/Details/5
        [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}")]
        [Authorize(Roles = "Administrador,Professor,Assistente,Aluno")]
        public async Task<IActionResult> Details(int? SubjectId, int? ModuleId)
        {
            if (ModuleId == null || SubjectId == null)
            {
                return NotFound();
            }

            var @module = await _context.Module
                    .Include(m => m.Subject)
                    .SingleOrDefaultAsync(m => m.Id == ModuleId);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Module/Create
        [HttpGet("/Subject/{SubjectId}/Module/Create")]
        [Authorize(Roles = "Administrador,Professor")]
        public IActionResult Create(int? SubjectId)
        {
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id");
            return View();
        }

        // POST: Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Subject/{SubjectId}/Module/Create")]
        [Authorize(Roles = "Administrador,Professor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? SubjectId,[Bind("Id,Name,Description,SubjectId,Active")] Module @module)
        {
            if (ModelState.IsValid && SubjectId != null)
            {
                @module.SubjectId = SubjectId;
                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", @module.SubjectId);
            return View(@module);
        }

        [HttpGet("/Subject/{SubjectId}/Module/Edit/{ModuleId}")]
        [Authorize(Roles = "Administrador,Professor")]
        // GET: Module/Edit/5
        public async Task<IActionResult> Edit(int? ModuleId)
        {
            if (ModuleId == null)
            {
                return NotFound();
            }

            var @module = await _context.Module.SingleOrDefaultAsync(m => m.Id == ModuleId);
            if (@module == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", @module.SubjectId);
            return View(@module);
        }

        // POST: Module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Subject/{SubjectId}/Module/Edit/{ModuleId}")]
        [Authorize(Roles = "Administrador,Professor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ModuleId, [Bind("Id,Name,Description,SubjectId,Active")] Module @module)
        {
            if (ModuleId != @module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
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
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", @module.SubjectId);
            return View(@module);
        }

        // GET: Module/Delete/5
        [HttpGet("/Subject/{SubjectId}/Module/Delete/{ModuleId}")]
        [Authorize(Roles = "Administrador,Professor")]
        public async Task<IActionResult> Delete(int? ModuleId)
        {
            if (ModuleId == null)
            {
                return NotFound();
            }

            var @module = await _context.Module
                    .Include(m => m.Subject)
                    .SingleOrDefaultAsync(m => m.Id == ModuleId);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Module/Delete/5
        [HttpPost("/Subject/{SubjectId}/Module/Delete/{ModuleId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Professor")]
        public async Task<IActionResult> DeleteConfirmed(int ModuleId)
        {
            var @module = await _context.Module.SingleOrDefaultAsync(m => m.Id == ModuleId);
            _context.Module.Remove(@module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return _context.Module.Any(e => e.Id == id);
        }
    }
}
