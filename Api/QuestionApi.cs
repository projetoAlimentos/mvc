using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class QuestionApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values
        [HttpGet("{topicId}")]
        public List<Question> GetAsync(int topicId)
        {
            var random = new Random("penis".GetHashCode());
            var questionList = _context.Question
                .Include(m => m.Answers)
                .Where(x => x.TopicId == topicId)
                .Where(x => x.Active == true)
                .ToList();

            var rng = new Random();
            int n = questionList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = questionList[k];
                questionList[k] = questionList[n];
                questionList[n] = value;
            }

            questionList = questionList.GetRange(0, 5);

            foreach (var questao in questionList)
            {
                var corretas = questao.Answers.Where(a => a.Correct);
                if (corretas.Count() == 1)
                    questao.VerdadeiraFalsa = true;
                else
                    questao.VerdadeiraFalsa = false;
            }

            return questionList;
        }

        // GET api/values
        [HttpGet("admin/{topicId}")]
        public Task<List<Question>> GetAsyncAdmin(int topicId)
        {
            var questionList = _context.Question
                .Include(m => m.Answers)
                .Where(x => x.TopicId == topicId);
            return questionList.ToListAsync();
        }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public async Task<Question> Get(int id)
        // {
        //     var question = await _context.Question
        //         .SingleOrDefaultAsync(m => m.Id == id);
        //     return question;
        // }

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
