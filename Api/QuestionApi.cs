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
using Newtonsoft.Json;
using projeto.Data;
using projeto.Models;
using projeto.Models.AccountViewModels;
using projeto.Services;

namespace projeto.Api
{

    [Route("api/[controller]")]
    public class QuestionApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values
        [HttpGet]
        public Task<List<Question>> GetAsync()
        {
            var questionList = _context.Question.Include(m => m.Answers);
            return questionList.ToListAsync();
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Question> Get(int id)
        {
            var question = await _context.Question
                .SingleOrDefaultAsync(m => m.Id == id);
            return question;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]Question question)
        {
            _context.Add(question);
            await _context.SaveChangesAsync();
        }

        // POST api/values
        [HttpPost("list")]
        public async void Post([FromBody] List<Question> questions)
        {
            foreach (var question in questions)
            {
                // try{
                    _context.Update(question);
                // } catch (Exception e) {
                    
                // }
            }
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Question question)
        {
            _context.Update(question);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var question = await _context.Question
                .Include(x => x.Answers)
                .SingleOrDefaultAsync(m => m.Id == id);
            
            foreach (var ans in question.Answers) {
                _context.Remove(ans);
            }
            _context.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}
