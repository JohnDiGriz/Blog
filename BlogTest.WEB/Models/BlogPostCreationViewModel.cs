using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BlogTest.WEB.Models
{
    public class BlogPostCreationViewModel //View-model for creating and editing posts
    {
        public bool IsEdit { get; set; } //Do we currently edit or create post

        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="Text Summary")]
        public string ShortText { get; set; }
        [Required]
        [Display(Name = "Full Text")]
        public string LongText { get; set; }
        [Required]
        [Url]
        [Display(Name = "Main Photo")]
        public string MainPicture { get; set; }
        public string AuthorId { get; set; }
        public DateTime? PostingTime { get; set; }
    }
}
