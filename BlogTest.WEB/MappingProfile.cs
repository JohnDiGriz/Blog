
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTest.WEB
{
    public class MappingProfile : Profile
    {
        public class DateFormatter : IValueConverter<DateTime, DateTime>
        {
            public DateTime Convert(DateTime source, ResolutionContext resolution)
                => source == null ? DateTime.Now : source;
        }
        public MappingProfile()
        {
            CreateMap<Microsoft.AspNetCore.Identity.IdentityUser, Models.UserViewModel>();
            CreateMap<DAL.Models.BlogPost, Models.BlogListItemViewModel>();
            CreateMap<DAL.Models.BlogPost, Models.BlogPostCreationViewModel>();
            CreateMap<DAL.Models.Comment, Models.CommentViewModel>();
            CreateMap<DAL.Models.BlogPost, Models.BlogPostViewModel>();
            CreateMap<Models.BlogPostCreationViewModel, DAL.Models.BlogPost>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Models.BlogListItemViewModel, DAL.Models.BlogPost>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}