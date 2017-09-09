namespace SportsStore.WebUI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Web.Mvc;
    using Concrete;
    using Contracts;
    using Data;
    using Data.Contracts;
    using Ninject;
    using Ninject.Web.Common;
    using SportsStore.Models.Entities;

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
            this._kernel.Bind<DbContext>().To<SportsShopContext>().InRequestScope();
            this._kernel.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            this._kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            this._kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}