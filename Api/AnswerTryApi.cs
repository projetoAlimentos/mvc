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
    public class AnswerTryTryApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswerTryTryApi(ApplicationDbContext context)
        {
          _context = context;
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<AnswerTry> Get(int id)
        {
            var answerTry = await _context.AnswerTry
                .SingleOrDefaultAsync(m => m.Id == id);
            return answerTry;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]AnswerTry answerTry)
        {
            _context.Add(answerTry);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]AnswerTry answerTry)
        {
            _context.Update(answerTry);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var answerTry = await _context.AnswerTry
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(answerTry);
            await _context.SaveChangesAsync();
        }
    }
}
