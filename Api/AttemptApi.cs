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
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class AttemptApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttemptApi(ApplicationDbContext context)
        {
          _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Attempt> GetAsync()
        {
            var attemptList = _context.Attempt;
            return attemptList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var attempt = await _context.Attempt
                .Include(x => x.AnswerAttempt)
                    .ThenInclude(x => x.Attempts)
                    .ThenInclude(x => x.Answer)
                .Include(x => x.AnswerAttempt)
                    .ThenInclude(x => x.Question)
                    .ThenInclude(x => x.Answers)
                .SingleOrDefaultAsync(m => m.Id == id);

            var acertos = 0;
            var erros = 0;

            // coisa horrivel para verificar os acertos
            foreach (var answer in attempt.AnswerAttempt) {
                var corretas = new List<int>();
                var usuario = new List<int>();

                foreach (var resposta in answer.Question.Answers) {
                    if (resposta.Correct)
                        corretas.Add(resposta.Id);
                }
                foreach (var tentativa in answer.Attempts)
                {
                    usuario.Add(tentativa.Answer.Id);
                }

                var a = corretas.Except(usuario);
                var b = usuario.Except(corretas);

                if (a.Count() == b.Count() && a.Count() == 0)
                    acertos++;
                else
                    erros++;
            }

            attempt.AnswerAttempt = null;

            return Ok(new {attempt, acertos, erros});
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Attempt attempt)
        {
            _context.Add(attempt);
            await _context.SaveChangesAsync();
            return Ok(new {idTentativa = attempt.Id});
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Attempt attempt)
        {
            _context.Update(attempt);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var attempt = await _context.Attempt
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(attempt);
            await _context.SaveChangesAsync();
        }
    }
}
