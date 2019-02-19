using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlogTest.DAL.Repositories
{
    class BlogPostsRepository : Repository<Models.BlogPost>, Interfaces.IBlogPostsRepository
    {
        public BlogPostsRepository(Data.ApplicationDbContext blogContext) : base(blogContext) { }
        public new Models.BlogPost Get(long id)
        {
            return DbSet_
                .Include(x => x.Comments)
                .Include(x => x.Author)
                .FirstOrDefault(x => x.Id == id);
        }
        public int GetPageCount(int onPage)
        {
            int count = DbSet_.Count();
            return count / onPage + count % onPage == 0 ? 0 : 1;
        }
        public IEnumerable<Models.BlogPost> GetPage(int page, int onPage)
        {
            int skippedCount = onPage * (page - 1);
            int postCount = DbSet_.Count();
            if (page > GetPageCount(onPage))
                return null;
            if (postCount == 0)
                return new List<Models.BlogPost>();
            return DbSet_.OrderByDescending(x=>x.PostingTime)
                .Skip(skippedCount)
                .Take(Math.Min(onPage, postCount - skippedCount))
                .Include(x=>x.Author)
                .Include(x=>x.Comments)
                .ToList();
        }
    }
}
