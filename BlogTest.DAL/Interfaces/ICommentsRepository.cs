using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BlogTest.DAL.Interfaces
{
    public interface ICommentsRepository : IRepository<Models.Comment>
    {
        int GetCommentsPageCount(long postId, int onPage);
        IEnumerable<Models.Comment> GetCommentsPage(long postId, int page, int onPage);
    }
}
