using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class CardPackController : Controller
    {
        public int PPagesize = 20;
        // GET: Cardpack
        public ActionResult PackOverview(bool? isCurrency, int page = 1)
        {
            List<CardPack> PackList = new List<CardPack>();
            var dbPacklist = PackManager.GetAllPacks();

            foreach (var p in dbPacklist)
            {
                CardPack pack = new CardPack();
                pack.IdPack = p.idpack;
                pack.PackName = p.packname;
                pack.IsMoney = p.ismoney;
                pack.PackPrice= (decimal)p.packprice;
                pack.Flavor = p.flavour;
                pack.Pic = p.packimage;
                PackList.Add(pack);
            }


            PacksListViewModel model = new PacksListViewModel()
            {
                Packs = PackList.OrderBy(c => c.IdPack)
                           .Where(p =>/* p.IsMoney == null ||*/ p.IsMoney == isCurrency)
                           .Skip((page - 1) * PPagesize)
                           .Take(PPagesize),
                PagingInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PPagesize,
                    TotalItems = PackList.Count()
                },
                CurrentClass =isCurrency
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            tblpack dbpack = null;

            dbpack = PackManager.GetPackById(id);

            CardPack pack = new CardPack();
            pack.IdPack = dbpack.idpack;
            pack.PackName = dbpack.packname;
            pack.PackPrice = (decimal)dbpack.packprice;
            pack.Pic = dbpack.packimage;
            pack.Flavor = dbpack.flavour;
            return View(pack);
        }
    }
}