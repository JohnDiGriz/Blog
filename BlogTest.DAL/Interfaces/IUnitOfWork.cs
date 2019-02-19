using System;
using System.Collections.Generic;
using System.Text;

namespace BlogTest.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogPostsRepository BlogPostsRepository { get; }
        ICommentsRepository CommentsRepository { get; }
        int Commit();
    }
}
