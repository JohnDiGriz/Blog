using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTest.WEB.Models
{
    public class CommentViewModel //One displayed comment
    {
        public long? Id { get; set; }
        public UserViewModel Author { get; set; }
        public string AuthorId { get; set; }
        public string Text { get; set; }
        public string DateTimeStr { get; set; }
    }
}
