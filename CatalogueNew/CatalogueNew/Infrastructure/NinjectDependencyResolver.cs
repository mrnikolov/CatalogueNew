﻿using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;

namespace CatalogueNew.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ILog>().ToMethod(x => LogManager.GetLogger(typeof(Controller)))
                .InSingletonScope();
            kernel.Bind(typeof(ICatalogueContext)).To(typeof(CatalogueContext))
                .InSingletonScope();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IManufacturerService>().To<ManufacturerService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();

        }
    }
}