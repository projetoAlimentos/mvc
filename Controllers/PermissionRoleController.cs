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
    public class PermissionRoleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermissionRoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PermissionRole
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PermissionRole.Include(p => p.Permission).Include(p => p.Role);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PermissionRole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionRole = await _context.PermissionRole
                .Include(p => p.Permission)
                .Include(p => p.Role)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (permissionRole == null)
            {
                return NotFound();
            }

            return View(permissionRole);
        }

        // GET: PermissionRole/Create
        public IActionResult Create()
        {
            ViewData["PermissionId"] = new SelectList(_context.Permission, "Id", "Id");
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "Id");
            return View();
        }

        // POST: PermissionRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleId,PermissionId")] PermissionRole permissionRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permissionRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PermissionId"] = new SelectList(_context.Permission, "Id", "Id", permissionRole.PermissionId);
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "Id", permissionRole.RoleId);
            return View(permissionRole);
        }

        // GET: PermissionRole/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionRole = await _context.PermissionRole.SingleOrDefaultAsync(m => m.Id == id);
            if (permissionRole == null)
            {
                return NotFound();
            }
            ViewData["PermissionId"] = new SelectList(_context.Permission, "Id", "Id", permissionRole.PermissionId);
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "Id", permissionRole.RoleId);
            return View(permissionRole);
        }

        // POST: PermissionRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,PermissionId")] PermissionRole permissionRole)
        {
            if (id != permissionRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permissionRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionRoleExists(permissionRole.Id))
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
            ViewData["PermissionId"] = new SelectList(_context.Permission, "Id", "Id", permissionRole.PermissionId);
            ViewData["RoleId"] = new SelectList(_context.Role, "Id", "Id", permissionRole.RoleId);
            return View(permissionRole);
        }

        // GET: PermissionRole/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionRole = await _context.PermissionRole
                .Include(p => p.Permission)
                .Include(p => p.Role)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (permissionRole == null)
            {
                return NotFound();
            }

            return View(permissionRole);
        }

        // POST: PermissionRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permissionRole = await _context.PermissionRole.SingleOrDefaultAsync(m => m.Id == id);
            _context.PermissionRole.Remove(permissionRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissionRoleExists(int id)
        {
            return _context.PermissionRole.Any(e => e.Id == id);
        }
    }
}
