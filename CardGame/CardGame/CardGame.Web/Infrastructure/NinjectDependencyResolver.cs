using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Moq;
using System.Linq;
using CardGame.Domain.Abstract;
using CardGame.Domain.Entities;

namespace CardGame.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        #region Dependency Resolver
        /// <summary>
        /// DOTO Comment
        /// </summary>
        /// <param name="kernelParam"></param>
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        } 
        #endregion

        #region Get Service
        /// <summary>
        /// TODO Comment
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        #endregion

        #region Get Services
        /// <summary>
        /// TODO Comment
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        #endregion

        #region Add Bindings
        /// <summary>
        ///Add your bindings here
        /// </summary>
        private void AddBindings()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { PackName= "BigPack",PackPrice=50,Quantity=10},
                new Product { PackName= "MediumPack",PackPrice=40,Quantity=8},
                new Product { PackName= "SmallPack",PackPrice=30,Quantity=6}
                });
            kernel.Bind<IProductsRepository>().ToConstant(mock.Object);
        } 
        #endregion
    }
}