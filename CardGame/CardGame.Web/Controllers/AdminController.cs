
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;
using System.Collections.Generic;
using log4net;
using System;
using System.Diagnostics;
using System.Web;

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
                var dbpacks = PackManager.AdminGetAllPacks();
                foreach (var pack in dbpacks)
                {
                    CardPack p = new CardPack();
                    p.IdPack = pack.ID;
                    p.IsActive = pack.IsActive ?? true;
                    p.IsMoney = pack.IsMoney ?? false;
                    p.NumCards = pack.NumberOfCards ?? 0;
                    p.PackName = pack.Name?? "n/a";
                    p.PackPrice = pack.Price ?? 0;
                    p.Worth = pack.Worth ?? 0;
                    p.Flavor = pack.FlavorText ?? "n/a";
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
                pack.IsActive = (bool)dbpack.IsActive;
                pack.IsMoney = dbpack.IsMoney ?? false;
                pack.NumCards = dbpack.NumberOfCards ?? 0;
                pack.PackName = dbpack.Name;
                pack.PackPrice = dbpack.Price ?? 0;
                pack.Worth = dbpack.Worth ?? 0;
                pack.Flavor = dbpack.FlavorText;
                //pack.Pic = dbpack.Image;
                //pack.ImageMimeType = dbpack.ImageMimeType;
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

        #region ACTIONRESULT EDIT II
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult Edit(CardPack cp, HttpPostedFileBase img = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        cp.ImageMimeType = img.ContentType;
                        cp.Pic = new byte[img.ContentLength];
                        img.InputStream.Read(cp.Pic, 0, img.ContentLength);
                    }
                    PackManager.SaveCardPack(cp.IdPack, cp.PackName, cp.Flavor, cp.IsMoney, cp.Worth, cp.NumCards, cp.IsActive, cp.Pic, cp.ImageMimeType);
                    TempData["message"] = string.Format("{0} wurde gespeichert", cp.PackName);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(cp);
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-EditII", e);
                return View("Error");
            }
        }
        #endregion

        #region VIEWRESULT CREATE
        public ViewResult Create()
        {
            return View("Edit", new CardPack());
        }
        #endregion

        #region ACTIONRESULT SET INACTIVE
        [HttpPost]
        public ActionResult SetInactive(int id)
        {
            var ok = PackManager.SetPackInactive(id);
            var Pack = PackManager.GetPackById(id);
            if (ok == true)
            {
                TempData["message"] = string.Format("{0} wurde inaktiv gesetzt", Pack.Name);
            }
            return RedirectToAction("Index");

        } 
        #endregion


        public FileContentResult GetImage(int id)
        {
            var pack = PackManager.GetPackById(id);
            if (pack != null)
            {
                return File(pack.Image, pack.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}