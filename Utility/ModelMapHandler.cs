using AutoMapper;
using System;

namespace Utility
{
    public static class ModelMapHandler
    {
        private static MapperConfiguration _config;

        public static TDestination BuildModelMapping<TSource, TDestination>(TSource source, TDestination destination) where TSource : class where TDestination : class
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(source.GetType(), destination.GetType());
            });

            var mapper = new Mapper(_config);
            return mapper.Map(source, destination);
        }
    }
}
