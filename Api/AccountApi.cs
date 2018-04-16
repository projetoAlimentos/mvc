using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using projeto.Data;
using projeto.Models;
using projeto.Models.AccountViewModels;
using projeto.Services;

namespace projeto.Api
{
    [Route("api/[controller]")]
    public class AccountApi : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountApi(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ApplicationUser> GetAsync()
        {
          var accountList = _context.ApplicationUser.Include(a => a.IdentityRole).Include(a => a.SubjectUser);
          return accountList.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ApplicationUser> Get(string id)
        {
          var applicationUser = await _context.ApplicationUser
              .Include(a => a.IdentityRole)
              .Include(a => a.SubjectUser)
              .SingleOrDefaultAsync(m => m.Id == id);
          if (applicationUser == null)
          {
            //return 404; //TODO: isso ae
            return null;
          }
          else 
          {
            return applicationUser;
          }
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]ApplicationUser applicationUser)
        {
          _context.Add(applicationUser);
          await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async void Put(string id, [FromBody]ApplicationUser applicationUser)
        {
          _context.Update(applicationUser);
          await _context.SaveChangesAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
          var applicationUser = await _context.ApplicationUser
              .Include(a => a.IdentityRole)
              .Include(a => a.SubjectUser)
              .SingleOrDefaultAsync(m => m.Id == id);
          _context.Remove(applicationUser);
          await _context.SaveChangesAsync();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<object> Post(
            [FromBody]LoginViewModel model,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                ApplicationUser applicationUser = _context.ApplicationUser
                    .SingleOrDefault(x => x.Email == model.Email.ToString());
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(applicationUser.Id, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, applicationUser.Id)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
          }

    }
}
