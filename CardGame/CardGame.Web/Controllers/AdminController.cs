
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;
using System.Collections.Generic;
using log4net;
using System;
using System.Diagnostics;

namespace CardGame.Web.Controllers
{
    public class AdminController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HomeController a = new HomeController();
        #region ACTIONRESULT INDEX
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            log.Info("AdminController-Index");

            try
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
                    p.Worth = pack.Worth ?? 0;
                    p.Flavor = pack.FlavorText;
                    Cardpacks.Add(p);
                }

                return View(Cardpacks);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-Edit", e);
                return View("Error");
            } 

        } 
        #endregion

        #region VIEWRESULT EDIT
        /// <summary>
        /// Takes the CardId, gets the Cardpack
        /// from DB,returns a ViewResult with pack
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ViewResult Edit(int id)
        {
            log.Info("AdminController-Edit");
            try
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
            catch (Exception e)
            {

                Debugger.Break();
                log.Error("AdminController-Edit", e);
                return View("Error");
            }

        } 
        #endregion
    }
}