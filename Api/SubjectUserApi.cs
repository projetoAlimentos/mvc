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
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class SubjectUserApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectUserApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<SubjectUser> GetAsync()
        {
            var subjectUserList = _context.SubjectUser;
            return subjectUserList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<SubjectUser> Get(int id)
        {
            var subjectUser = await _context.SubjectUser
                .SingleOrDefaultAsync(m => m.Id == id);
            return subjectUser;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]SubjectUser subjectUser)
        {
            _context.Add(subjectUser);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]SubjectUser subjectUser)
        {
            _context.Update(subjectUser);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var subjectUser = await _context.SubjectUser
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(subjectUser);
            await _context.SaveChangesAsync();
        }
    }
}
