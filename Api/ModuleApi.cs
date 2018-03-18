using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using projeto.Data;
using projeto.Models;
using projeto.Models.AccountViewModels;
using projeto.Services;

namespace projeto.Api
{

    [Route("api/[controller]")]
    public class ModuleApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModuleApi(ApplicationDbContext context)
        {
          _context = context;
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<Module> GetAsync()
        {
            var moduleList = _context.Module;
            return moduleList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Module> Get(int id)
        {
            var module = await _context.Module
                .SingleOrDefaultAsync(m => m.Id == id);
            return module;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]Module module)
        {
            _context.Add(module);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Module module)
        {
            _context.Update(module);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var module = await _context.Module
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(module);
            await _context.SaveChangesAsync();
        }
    }
}
