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
    public class ArticleApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Article> GetAsync()
        {
            var articleList = _context.Article;
            return articleList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Article> Get(int id)
        {
            var article = await _context.Article
                .SingleOrDefaultAsync(m => m.Id == id);
            return article;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]Article article)
        {
            _context.Add(article);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Article article)
        {
            _context.Update(article);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var article = await _context.Article
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}
