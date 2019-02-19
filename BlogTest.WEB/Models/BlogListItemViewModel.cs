using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTest.WEB.Models
{
    public class BlogListItemViewModel //View-model for one post on home page
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortText { get; set; }
        public string MainPicture { get; set; }
        public UserViewModel Author { get; set; }

        public string DateTimeStr { get; set; }

        public int CommentsCount { get; set; } 
    }
}
