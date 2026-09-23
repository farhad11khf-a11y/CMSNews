using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CMSNews.Models.Models;
using CMSNews.Models.ViewModels;

namespace CMSNews.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper mapper;
        public static void MapperConfiguration()
        {
            MapperConfiguration config = GetConfig();
            mapper = config.CreateMapper();
        }
        private static MapperConfiguration GetConfig()
        {
            return new MapperConfiguration(t =>
            {
                t.CreateMap<NewsGroup, NewsGroupsViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
                t.CreateMap<News , NewsViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
                t.CreateMap<User, UserViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
                t.CreateMap<Comment, CommentsViewMdel>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
                
            });
        }
    }
}