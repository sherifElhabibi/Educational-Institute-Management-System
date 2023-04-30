using Assignemnt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assignemnt.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITIContext _context;

        public AccountController(ITIContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string returnurl)
        {
            if (ModelState.IsValid) {
                var search = _context.Student.Include(a=>a.Roles).FirstOrDefault(a => a.Email == model.Email && a.Password==model.Password);

                if(search!=null) { 
                Claim c1 = new Claim(ClaimTypes.Name,search.StdName);
                Claim c2 = new Claim(ClaimTypes.Email, search.Email);

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                claimsIdentity.AddClaim(c1);
                claimsIdentity.AddClaim(c2);
                    foreach (var role in search.Roles)
                    {
                        Claim c3 = new Claim(ClaimTypes.Role, role.RoleName);
                        claimsIdentity.AddClaim(c3);
                    }
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                claimsPrincipal.AddIdentity(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                    if(returnurl!=null)
                    {
                        return LocalRedirect(returnurl);
                    }
                return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Email or Password Is Incorrect");
            }
                return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        // GET: Students/Create
        public IActionResult Register()
        {
            var depts = _context.Department.ToList();
            ViewBag.Department = depts;

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Student student, int DeptId)
        {
            var depts = _context.Department.ToList();
            var dept = _context.Department.FirstOrDefault(a => a.DeptId == DeptId);
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                dept.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Department = depts;
            return View(student);
        }
    }
}
