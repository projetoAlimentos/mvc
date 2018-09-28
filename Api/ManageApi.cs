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
    public class ManageApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageApi(ApplicationDbContext context)
        {
          _context = context;
        }
    }
}
