namespace SportsStore.WebUI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Web.Mvc;
    using Data;
    using Data.Contracts;
    using SportsStore.Models.Entities;
    using Moq;
    using Ninject;
    using Ninject.Web.Common;
    using SportsStore.Models.Contracts;

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            this._kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return this._kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();

            //mock.Setup(m => m.Products).Returns(new List<IProduct>
            //{
            //    new Product {Name = "Football", Price = 25},
            //    new Product { Name = "Surf board", Price = 179 },
            //    new Product { Name = "Running shoes", Price = 95 }
            //});

            //this._kernel.Bind<IProductRepository>().ToConstant(mock.Object);
            this._kernel.Bind<IProduct>().To<Product>();
            this._kernel.Bind<DbContext>().To<SportsShopContext>().InRequestScope();
            this._kernel.Bind(typeof(IGenericRepository<>)).To(typeof(EfProductGenericRepository<>));
        }
    }
}