using System;
using System.Collections.Generic;
using System.Text;

namespace BlogTest.DAL.Interfaces
{
    public interface IBlogPostsRepository : IRepository<Models.BlogPost>
    {
        IEnumerable<Models.BlogPost> GetPage(int page, int onPage);
        int GetPageCount(int onPage);
    }
}
