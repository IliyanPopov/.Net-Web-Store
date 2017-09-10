namespace SportsStore.WebUI
{
    using System.IO;
    using System.Web;
    using AutoMapper;
    using SportsStore.Models.Entities;
    using ViewModels;

    public static class AutoMapperConfig
    {
        public static void RegisterAllMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductEditViewModel>()
                .ForMember(vm => vm.ImageData, opt => opt.MapFrom(src => new byte[0]));


                cfg.CreateMap<ProductEditViewModel, Product>()
                   .ForMember(vm => vm.ImageData, opt => opt.MapFrom(src => new byte[0]));

                cfg.AllowNullCollections = true;
            });
        }

        //private static byte[] GetBytesFromFile(HttpPostedFileBase file)
        //{
        //    if (file == null)
        //    {
        //        return new byte[0];
        //    }

        //    MemoryStream stream = new MemoryStream();
        //    file.InputStream.CopyTo(stream);
        //    byte[] pictureInData = stream.ToArray();

        //    return pictureInData;
        //}
    }
}