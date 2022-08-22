using Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly Assignment2DbContext _context;
        public PostRepository(Assignment2DbContext context)
        {
            _context = context;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public List<Post> GetAll()
        {
            return _context.Posts.Include(x => x.Category).ToList();
        }
        public Post Create(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }
        public void Update(Post post)
        {

            _context.Update(post);
            _context.SaveChanges();
        }

        public Post? GetById(int id)
        {
            return _context.Posts.Find(id);
        }

        public void Delete(int id)
        {
            var post = _context.Posts.Find(id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
        public (List<Post>, int) GetPostsWithPaged(int page, int pageSize)
        {
            var posts = GetAll();
            int totalCount = posts.Count;
            var ListedPosts = posts.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            return (ListedPosts, totalCount);
        }
    }
}
