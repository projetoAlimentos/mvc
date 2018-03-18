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
    public class TopicApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopicApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Topic> Get(int id)
        {
            var topic = await _context.Topic
                .SingleOrDefaultAsync(m => m.Id == id);
            return topic;
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
