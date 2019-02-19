using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTest.DAL.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; } //Comment text
        public DateTime PostingTime { get; set; } //Time of creation

        public long PostId { get; set; }
        public BlogPost Post { get; set; }

        public string AuthorId { get; set; }
        public IdentityUser Author { get; set; }

        [NotMapped]
        public string DateTimeStr { get { return PostingTime.ToLongDateString(); } }
    }
}
