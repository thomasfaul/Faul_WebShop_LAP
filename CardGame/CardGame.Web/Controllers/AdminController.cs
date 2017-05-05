
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;
using System.Collections.Generic;

namespace CardGame.Web.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            List<CardPack> Cardpacks = new List<CardPack>();
            var dbpacks = PackManager.GetAllPacks();
            foreach (var pack in dbpacks)
            {
                CardPack p = new CardPack();
                p.IdPack = pack.ID;
                p.IsMoney = pack.IsMoney ?? false;
                p.NumCards = pack.NumberOfCards ?? 0;
                p.PackName = pack.Name;
                p.PackPrice = pack.Price ?? 0;
                p.Worth = pack.Worth?? 0;
                p.Flavor = pack.FlavorText;
                Cardpacks.Add(p);
            }

            return View(Cardpacks);
        }
        public ViewResult Edit(int id)
        {
            CardPack pack = new CardPack();
            var dbpack = PackManager.GetPackById(id);
            pack.IdPack = dbpack.ID;
            pack.IsMoney = dbpack.IsMoney ?? false;
            pack.NumCards = dbpack.NumberOfCards ?? 0;
            pack.PackName = dbpack.Name;
            pack.PackPrice = dbpack.Price ?? 0;
            pack.Worth = dbpack.Worth ?? 0;
            pack.Flavor = dbpack.FlavorText;
            return View(pack);

        }
    }
}