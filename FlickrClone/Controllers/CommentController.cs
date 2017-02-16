using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlickrClone.Models;
using Microsoft.AspNetCore.Identity;

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
        public async Task<IActionResult> Create(string commentContent, int photoId)
        {
            
            Comment Comment = new Comment();
            Comment.Content = commentContent;
            Comment.PhotoId = photoId;
            Comment.Date = DateTime.Now;
            Comment.User = await _userManager.GetUserAsync(User);
            _db.Comments.Add(Comment);
            _db.SaveChanges();

            return View("/Photos/Details/photoId");
        }
    }
}
