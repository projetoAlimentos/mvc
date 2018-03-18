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
    public class AccountApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ApplicationUser> GetAsync()
        {
          var accountList = _context.ApplicationUser.Include(a => a.IdentityRole).Include(a => a.SubjectUser);
          return accountList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ApplicationUser> Get(string id)
        {
          var applicationUser = await _context.ApplicationUser
              .Include(a => a.IdentityRole)
              .Include(a => a.SubjectUser)
              .SingleOrDefaultAsync(m => m.Id == id);
          if (applicationUser == null)
          {
            //return 404; //TODO: isso ae
            return null;
          }
          else 
          {
            return applicationUser;
          }
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]ApplicationUser applicationUser)
        {
          _context.Add(applicationUser);
          await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(string id, [FromBody]ApplicationUser applicationUser)
        {
          _context.Update(applicationUser);
          await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
          var applicationUser = await _context.ApplicationUser
              .Include(a => a.IdentityRole)
              .Include(a => a.SubjectUser)
              .SingleOrDefaultAsync(m => m.Id == id);
          _context.Remove(applicationUser);
          await _context.SaveChangesAsync();
        }
    }
}
