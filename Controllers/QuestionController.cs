using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projeto.Data;
using projeto.Models;

namespace projeto.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Question
        [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question")]
        [Authorize(Roles = "Administrador,Professor,Assistente,Aluno")]
        public async Task<IActionResult> Index(int? TopicId, int? ModuleId)
        {
            if (TopicId == null)
            {
                var applicationDbContext = _context.Question.Include(q => q.Topic);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                ViewData["ModuleId"] = ModuleId;
                ViewData["TopicId"] = TopicId;

                var applicationDbContext = _context.Question.Include(q => q.Topic).Include(q => q.Topic.Module).
                        Where(q => q.TopicId == TopicId);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Question/Details/5
        [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/{QuestionId}")]
        [Authorize(Roles = "Administrador,Professor,Assistente,Aluno")]
        public async Task<IActionResult> Details(int? QuestionId)
        {
            if (QuestionId == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                    .Include(q => q.Topic)
                    .SingleOrDefaultAsync(m => m.Id == QuestionId);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Question/Create
        [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/Create")]
        [Authorize(Roles = "Administrador,Professor,Assistente")]
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Professor,Assistente")]
        public async Task<IActionResult> Create(int TopicId, [Bind("Id,Difficulty,Description,Hint,Active,TopicId")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.TopicId = TopicId;
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", question.TopicId);
            return View(question);
        }

        // GET: Question/Edit/5
        [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/Edit/{QuestionId}")]
        [Authorize(Roles = "Administrador,Professor,Assistente")]
        public async Task<IActionResult> Edit(int? QuestionId)
        {
            if (QuestionId == null)
            {
                return NotFound();
            }

            var question = await _context.Question.SingleOrDefaultAsync(m => m.Id == QuestionId);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", question.TopicId);
            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/Edit/{QuestionId}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Professor,Assistente")]
        public async Task<IActionResult> Edit(int QuestionId, [Bind("Id,Difficulty,Description,Hint,Active,TopicId")] Question question)
        {
            if (QuestionId != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", question.TopicId);
            return View(question);
        }

        // GET: Question/Delete/5
        [HttpGet("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/Delete/{QuestionId}")]
        [Authorize(Roles = "Administrador,Professor")]
        public async Task<IActionResult> Delete(int? QuestionId)
        {
            if (QuestionId == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                    .Include(q => q.Topic)
                    .SingleOrDefaultAsync(m => m.Id == QuestionId);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost("/Subject/{SubjectId}/Module/{ModuleId}/Topic/{TopicId}/Question/Delete/{QuestionId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Professor")]
        public async Task<IActionResult> DeleteConfirmed(int QuestionId)
        {
            var question = await _context.Question.SingleOrDefaultAsync(m => m.Id == QuestionId);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.Id == id);
        }

        // GET api/values
        [HttpGet("/Question/{topicId}")]
        [Authorize(Roles = "Administrador,Professor")]
        public Task<List<Question>> GetAsyncJson(int topicId)
        {
            var questionList = _context.Question
                .Include(m => m.Answers)
                .Where(x => x.TopicId == topicId)
                .Where(x => x.Active == true);
            return questionList.ToListAsync();
        }

        // GET api/values
        [HttpGet("/Question/admin/{topicId}")]
        [Authorize(Roles = "Administrador,Professor")]
        public Task<List<Question>> GetAsyncAdminJson(int topicId)
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
        [HttpPost("/Question")]
        [Authorize(Roles = "Administrador,Professor")]
        public async void PostJson([FromBody]Question question)
        {
            _context.Add(question);
            await _context.SaveChangesAsync();
        }

        // POST api/values
        [HttpPost("/Question/list")]
        [Authorize(Roles = "Administrador,Professor")]
        public async void PostJson([FromBody] List<Question> questions)
        {
            foreach (var question in questions)
            {
                _context.Update(question);
            }
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("/Question/edit/{id}")]
        [Authorize(Roles = "Administrador,Professor")]
        public async void PutJson(int id, [FromBody]Question question)
        {
            _context.Update(question);
            await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("/Question/delete/{id}")]
        [Authorize(Roles = "Administrador,Professor")]
        public async void DeleteJson(int id)
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
