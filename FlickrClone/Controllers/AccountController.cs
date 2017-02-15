using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using FlickrClone.Models;
using FlickrClone.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;



namespace FlickrClone.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController()
        { }
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        public IActionResult Index(ApplicationUser user)
        {
            if(user != null)
            {
                return View(user);
            }
            else
            {
            return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, IFormFile picture, string password)
        {
            byte[] pictureArray = new byte[0];
            if(picture.Length > 0)
            {
                using (var fileStream = picture.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    pictureArray = ms.ToArray();
                }
            }

            var user = new ApplicationUser { UserName = username, Email = email, ProfilePicture = pictureArray };
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                var user = _userManager.GetUserAsync(User);
                return View("Index", user);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            string userId = user.Id;
            return View(user);
        }
    }
    
}
