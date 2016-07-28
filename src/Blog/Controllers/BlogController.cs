using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Blog.Models.PostViewModels;
using Blog.Models;
using Blog.Models.Account;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext _context { get; } 

        public BlogController (BlogContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
       

        [HttpGet("")]
        public IActionResult Index()
        {
            return Pager(1);
        }

        [HttpGet("[controller]/{page:int?}")]
        public IActionResult Pager(int page)
        {
 
             var result = new PageResult(_context, page);             
            return View("Index", result);
        }

        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult SuccessContact(ContactViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost("[controller]/contact")]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var name = viewModel.Name;
                return RedirectToActionPermanent("SuccessContact", viewModel);
            }

            return View();
        }


       
       
        public IActionResult Create()
        {
            var post = new PostViewModel()
            {
                Title = "MyPost",
                ShortDescription = "Hello world",
                Description = "Hey there!"

            };
            var newPost = new Post(post);
            newPost.IsPublished = true;
            newPost.PostedOn = DateTime.UtcNow;
            try
            {
                _context.Add(newPost);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return View(e.Message);
            }
            return RedirectToAction("Index");
        }


        public IActionResult CreatePost()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
         

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostViewModel post)
        {
            if(ModelState.IsValid)
            {
                var newPost = new Post(post);
                newPost.IsPublished = true;
                _context.Posts.Add(newPost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }
    }
}
