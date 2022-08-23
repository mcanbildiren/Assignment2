using Assignment2.Models;
using Assignment2.Models.ViewModels;
using Assignment2.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace Assignment2.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;

        public PostController(IPostRepository postRepository, IMapper mapper, IFileProvider fileProvider)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public IActionResult AdminDashboard(int page = 1)
        {
            var pageSize = 5;
            var postList = _mapper.Map<List<PostViewModel>>(_postRepository.GetPostsWithPaged(page, pageSize).Item1);
            var totalCount = _postRepository.GetPostsWithPaged(page, pageSize).Item2;
            int totalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
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

            PhotoSave(request.Photo);

            var fileName = request.Photo.FileName;

            var newPost = _mapper.Map<Post>(request);
            newPost.Photo = fileName;
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

        [HttpGet]
        public IActionResult PostListWithPaging(int page, int pageSize)
        {
            ViewBag.Page = page;

            var postWithPaged = _postRepository.GetAll().Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var totalCount = _postRepository.GetAll().Count;

            return View((postWithPaged, totalCount));
        }

        [HttpGet]
        public IActionResult PhotoSave()
        {
            return View();
        }

        [HttpPost]
        public async void  PhotoSave(IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var picturesDirectory = root.Single(x => x.Name == "Pictures");

                var path = Path.Combine(picturesDirectory.PhysicalPath, photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream);
            }
        }
    }
}
