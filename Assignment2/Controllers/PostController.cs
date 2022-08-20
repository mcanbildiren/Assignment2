using Assignment2.Models;
using Assignment2.Models.ViewModels;
using Assignment2.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment2.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            var postList = _mapper.Map<List<PostViewModel>>(_postRepository.GetAll());
            return View(postList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categoryList = _postRepository.GetCategories();

            ViewBag.selectList = new SelectList(categoryList, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var categoryList = _postRepository.GetCategories();

                ViewBag.selectList = new SelectList(categoryList, "Id", "Name");

                return View(request);
            }
            var newPost = _mapper.Map<Post>(request);
            _postRepository.Create(newPost);
            return RedirectToAction("AdminDashboard", "Post");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var categoryList = _postRepository.GetCategories();

            ViewBag.selectList = new SelectList(categoryList, "Id", "Name");

            var postUpdateViewModel = _mapper.Map<PostUpdateViewModel>(_postRepository.GetById(id));

            return View(postUpdateViewModel);
        }

        [HttpPost]
        public IActionResult Update(PostUpdateViewModel request)
        {
            if (!ModelState.IsValid)
            {

                var categoryList = _postRepository.GetCategories();

                ViewBag.selectList = new SelectList(categoryList, "Id", "Name");
                return View();
            }

            _postRepository.Update(_mapper.Map<Post>(request));
            return RedirectToAction("AdminDashboard", "Post");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _postRepository.Delete(id);
            return RedirectToAction("AdminDashboard", "Post");
        }
    }
}
