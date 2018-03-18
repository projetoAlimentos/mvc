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
    public class ResponsableSubjectApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponsableSubjectApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ResponsableSubject> GetAsync()
        {
            var responsableSubjectList = _context.ResponsableSubject;
            return responsableSubjectList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ResponsableSubject> Get(int id)
        {
            var responsableSubject = await _context.ResponsableSubject
                .SingleOrDefaultAsync(m => m.Id == id);
            return responsableSubject;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]ResponsableSubject responsableSubject)
        {
            _context.Add(responsableSubject);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]ResponsableSubject responsableSubject)
        {
            _context.Update(responsableSubject);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var responsableSubject = await _context.ResponsableSubject
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(responsableSubject);
            await _context.SaveChangesAsync();
        }
    }
}
