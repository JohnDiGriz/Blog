using System;
using System.Collections.Generic;
using System.Text;
using BlogTest.DAL.Interfaces;

namespace BlogTest.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Data.ApplicationDbContext Context_;
        public IBlogPostsRepository BlogPostsRepository { get; private set; }
        public ICommentsRepository CommentsRepository { get; private set; }
        public UnitOfWork(Data.ApplicationDbContext context)
        {
            Context_ = context;
            BlogPostsRepository = new Repositories.BlogPostsRepository(context);
            CommentsRepository = new Repositories.CommentsRepository(context);
        }
        public int Commit()
        {
            return Context_.SaveChanges();
        }
        public void Dispose()
        {
            Context_.Dispose();
        }
    }
}
