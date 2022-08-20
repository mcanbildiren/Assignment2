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

        public IActionResult Index()
        {
            var postList = _mapper.Map<List<PostViewModel>>(_postRepository.GetAll());
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
    }
}