namespace SportsStore.WebUI
{
    using System.Reflection;
    using AutoMapper;
    using Data.Contracts;
    using SportsStore.Models.Entities;
    using ViewModels;


    public static class AutoMapperConfig
    {
        public static void RegisterAllMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductEditViewModel>();

                cfg.CreateMap<ProductEditViewModel, Product>();
            });
        }
    }
}