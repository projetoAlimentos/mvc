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
    public class TopicApi : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TopicApi(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Topic> GetAsync()
        {
            var topicList = _context.Topic;
            return topicList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var topicList = _context.Topic
                .Where(m => m.ModuleId == id)
                .ToList()
                .OrderBy(t => t.Order);

            var attAPI = new AttemptApi(_context);
            var topics = new List<Object>();

            foreach (var topic in topicList)
            {
                var userId = HttpContext.User.Identity.Name;

                var attemptList = _context.Attempt
                    .Where(a => a.TopicId == topic.Id && a.User.Id == userId)
                    .ToList();

                int acertosMax = 0;
                int errosMax = 0;

                foreach (var attempt in attemptList)
                {
                    var attemptStuff = await attAPI.GenerateAns(attempt.Id);
                    var acertos = attemptStuff.GetType().GetProperty("acertos").GetValue(attemptStuff, null);
                    var erros = attemptStuff.GetType().GetProperty("erros").GetValue(attemptStuff, null);

                    // Pega o índice 1 do objeto anonimo
                    if ((int) acertos > acertosMax || acertosMax == 0) {
                        acertosMax = (int)acertos;
                        errosMax = (int)erros;
                    }
                }

                topics.Add(new {topic, acertosMax, errosMax});
            }

            return Ok(topics);
        }


                // GET api/values/5
        [HttpGet("single/{id}")]
        public Task<Topic> GetSingle(int id)
        {
            var topicList = _context.Topic
                .Where(m => m.Id == id).SingleOrDefaultAsync();
            return topicList;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]Topic topic)
        {
            _context.Add(topic);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Topic topic)
        {
            _context.Update(topic);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var topic = await _context.Topic
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(topic);
            await _context.SaveChangesAsync();
        }
    }
}
