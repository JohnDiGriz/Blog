using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTest.WEB.Models
{
    public class BlogPostViewModel //Details of opened post
    {
        public BlogPostViewModel()
        {
            DisplayedComments = new List<CommentViewModel>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string LongText { get; set; }
        public string MainPicture { get; set; }
        public UserViewModel Author { get; set; }
        public string DateTimeStr { get; set; }

        public List<CommentViewModel> DisplayedComments { get; set; }//Comments that will be displayed
        public CommentViewModel AddedComment { get; set; } //Comment user might add
        
        public bool IsAdmin { get; set; } //Is current user admin
        public string CurrentUser { get; set; } //Current user Id

        public int PageNum { get; set; }
        public int PageCount { get; set; }


    }
}
