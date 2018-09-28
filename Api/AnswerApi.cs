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
    public class AnswerApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswerApi(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Answer> GetAsync()
        {
            var answerList = _context.Answer;
            return answerList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Answer> Get(int id)
        {
            var answer = await _context.Answer
                .SingleOrDefaultAsync(m => m.Id == id);
            return answer;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]Answer answer)
        {
            _context.Add(answer);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Answer answer)
        {
            _context.Update(answer);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var answer = await _context.Answer
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(answer);
            await _context.SaveChangesAsync();
        }
    }
}
