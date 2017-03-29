
using System.Web.Mvc;
using CardGame.DAL.Logic;
using PagedList;

namespace CardGame.Web.Controllers
{
    public class PaginationController : Controller
    {
        // GET: Pagination
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult PaginationPicList(int? pagenumber, int pagesize)
        {
            var pageNumber = pagenumber ?? 1;
            var pageSize = pagesize;

            var PicList = CardManager.GetAllCards();
            PicList.ToPagedList(pageNumber,pageSize);
            return PartialView(PicList);
        }
    }
}