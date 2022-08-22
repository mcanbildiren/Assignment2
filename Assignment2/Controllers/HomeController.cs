using System.Diagnostics;
using Assignment2.Models;
using Assignment2.Models.ViewModels;
using Assignment2.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public HomeController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public IActionResult Index(int page =1 )
        {
            var pageSize = 3;
            var postList = _mapper.Map<List<PostViewModel>>(_postRepository.GetPostsWithPaged(page,pageSize).Item1);
            var totalCount = _postRepository.GetPostsWithPaged(page, pageSize).Item2;
            int totalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
            return View(postList);
        }

        [Route("hakkimizda")]
        public IActionResult About()
        {
            return View();
        }

        [Route("iletisim")]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult DetailedPage(int id)
        {

            var post = _mapper.Map<PostViewModel>(_postRepository.GetById(id));
            return View(post);
        }
    }
}