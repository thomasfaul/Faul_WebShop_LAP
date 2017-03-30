using System.Web.Mvc;
using CardGame.Domain.Abstract;
using CardGame.Domain.Entities;

namespace CardGame.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository _repository;

        #region Product Controller
        /// <summary>
        /// declares a dependency on the IProductRepositry Interface which will lead Ninject to injct the dependency for the product repository when it instantiats the controller class
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductController(IProductsRepository productRepository)
        {
            this._repository = productRepository;
        }
        #endregion

        #region ViewResult ProductList()
        /// <summary>
        /// call the View without specifying a view name  tells the framework to render the default view for the action method. 
        /// </summary>
        /// <returns></returns>
        public ViewResult ProductList()
        {
            return View(_repository.Products);
        } 
        #endregion
    }
}