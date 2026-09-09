using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


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
            });
        }
    }
}