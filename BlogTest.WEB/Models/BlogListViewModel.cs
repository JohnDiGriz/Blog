using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTest.WEB.Models
{
    public class BlogListViewModel //View model for list of posts on home page
    {
        public BlogListViewModel()
        {
            BlogPosts = new List<BlogListItemViewModel>();
        }
        public List<BlogListItemViewModel> BlogPosts { get; set; }
        
        public int PageNum { get; set; }
        public int PageCount { get; set; }
        public bool IsAdmin { get; set; } //Is current user has admin role
    }
}
