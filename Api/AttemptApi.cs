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
    public class AttemptApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttemptApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Attempt> GetAsync()
        {
            var attemptList = _context.Attempt;
            return attemptList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Attempt> Get(int id)
        {
            var attempt = await _context.Attempt
                .SingleOrDefaultAsync(m => m.Id == id);
            return attempt;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]Attempt attempt)
        {
            _context.Add(attempt);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Attempt attempt)
        {
            _context.Update(attempt);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var attempt = await _context.Attempt
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(attempt);
            await _context.SaveChangesAsync();
        }
    }
}
