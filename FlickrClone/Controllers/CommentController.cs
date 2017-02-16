using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlickrClone.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace FlickrClone.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public CommentController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(string content, int photoId)
        {
            Debug.WriteLine("content");          
            Comment Comment = new Comment(content, photoId);
            Comment.User = await _userManager.GetUserAsync(User);
            _db.Comments.Add(Comment);
            _db.SaveChanges();

            return RedirectToAction("Details", "Photos", new { id = photoId });
        }
    }
}
