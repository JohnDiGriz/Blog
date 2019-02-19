﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BlogTest.DAL.Repositories
{
    class CommentsRepository : Repository<Models.Comment>, Interfaces.ICommentsRepository
    {
        public CommentsRepository(Data.ApplicationDbContext blogContext) : base(blogContext) { }
        public int GetCommentsPageCount(long postId, int onPage)
        {
            int count = DbSet_.Where(x => x.PostId == postId).Count();
            return count / onPage + (count % onPage == 0 ? 0 : 1);
        } 
        public IEnumerable<Models.Comment> GetCommentsPage(long postId, int page, int onPage)
        {
            if (page > GetCommentsPageCount(postId, onPage))
                return null;
            return DbSet_
                .Where(x => x.PostId == postId)
                .OrderByDescending(x => x.PostingTime)
                .Skip(onPage * (page - 1)).Take(onPage)
                .ToList();
        }

        private IEnumerable<Models.Comment> GetPage(IQueryable<Models.Comment> query, int page, int onPage)
        {
            return query.OrderByDescending(x => x.PostingTime).Skip(onPage * (page - 1)).Take(onPage).ToList();
        }
    }
}
