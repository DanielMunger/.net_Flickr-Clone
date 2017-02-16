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
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FlickrClone.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;
        public PhotosController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.Photos = _db.Photos.ToList();
            return View();
        }

        public IActionResult Details(int id)
        {
            
            return View(_db.Photos
                .Include(photo => photo.Comments)
                .ThenInclude(comment => comment.User)
                .FirstOrDefault(photo => photo.PhotoId == id));
        }

        public IActionResult AddPhoto()
        {
            return View(_db.Users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(IFormFile picture, string[] taggedUser)
        {
            foreach(var user in taggedUser)
            {
                Debug.WriteLine("this is a user " + user);
            }
            var pictureArray = new byte[0];
            if (picture.Length > 0)
            {
                using (var fileStream = picture.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    pictureArray = ms.ToArray();
                }
                Photo photo = new Photo(pictureArray);
                var user = await _userManager.GetUserAsync(User);

                photo.User = user;
                _db.Photos.Add(photo);
                _db.SaveChanges();

                //not working currently - can only tag one user with no for loop
                if (taggedUser != null)
                {
                    foreach(var tagUser in taggedUser)
                    {
                       
                        User_Tag user_tag = new User_Tag(tagUser, photo.PhotoId);
                        _db.User_Tags.Add(user_tag);
                        _db.SaveChanges();

                    }
                }
            }
            

            return RedirectToAction("Index");
              
        }
    }
}
