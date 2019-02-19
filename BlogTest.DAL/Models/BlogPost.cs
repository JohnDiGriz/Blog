using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace BlogTest.DAL.Models
{
    public class BlogPost
    {
        public long Id { get; set; }
        public string Name { get; set; } //Title of post
        public string ShortText { get; set; } //What people see in blogpost list
        public string LongText { get; set; } //Full post
        public string MainPicture { get; set; } //Main picture in post
        public DateTime PostingTime { get; set; } //Time of creation

        public ICollection<Comment> Comments { get; set; } 
        public string AuthorId { get; set; }
        public IdentityUser Author { get; set; }

        [NotMapped]
        public string DateTimeStr { get { return PostingTime.ToString(); } }

    }
}
