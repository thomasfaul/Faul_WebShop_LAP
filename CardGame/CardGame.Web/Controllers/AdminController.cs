
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;
using System.Collections.Generic;
using log4net;
using System;
using System.Diagnostics;
using System.Web;
using CardGame.Web.Controllers.HtmlHelpers;
using CardGame.Web.Models.DB;
using CardGame.Web.Models.UI;

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
        public ActionResult Index(int? sortValue, string search)
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
                var sorted = Cardpacks;
                if (sortValue != null)
                {
                    sorted = SortHelper.FilterCardPacks(Cardpacks, (int)sortValue);
                }
                if (search != null)
                {
                    sorted = SortHelper.FilterCardPacks(Cardpacks,search);
                }
                return View(sorted);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-Edit", e);
                return View("Error");
            }

        }
        #endregion

        #region ACTIONRESULT A_USER INDEX
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult A_UserIndex(int? sortValue, string search)
        {
            log.Info("AdminController-Index");

            try
            {
                List<AdminUserInfo> Users = new List<AdminUserInfo>();
                var dbuser = UserManager.GetAllUser();
                foreach (var user in dbuser)
                {
                    AdminUserInfo u = new AdminUserInfo();
                    u.ID = user.ID;
                    u.IsActive = user.IsActive ?? true;
                    u.Firstname = user.FirstName ?? "n/a";
                    u.Lastname = user.LastName ?? "n/a";
                    u.Gamertag = user.GamerTag ?? "n/a";
                    u.Email = user.Email ?? "n/a";
                    u.CurrencyBalance = user.AmountMoney ?? 0;
                    u.userrole = user.UserRole ?? "n/a";
                    u.Pic = user.Avatar ;
                    u.ImageMimeType = user.AvatarMimeType ?? "n/a";
                    u.BanDate = user.BanDate ?? DateTime.MinValue;
                    u.EntryDate = user.EntryDate ?? DateTime.MinValue;
                    Users.Add(u);
                }
                var sorted = Users;
                if (sortValue != null)
                {
                    sorted = SortHelper.FilterUsers(Users, (int)sortValue);
                }
                if (search != null)
                {
                    sorted = SortHelper.FilterUsers(Users, search);
                }
                return View(sorted);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-A_UserIndex", e);
                return View("Error");
            }

        }
        #endregion

        #region ACTIONRESULT CARD INDEX
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult CardIndex(int? sortValue,string search)
        {
            log.Info("AdminController-CardIndex");

            try
            {
                List<Card> Cards = new List<Card>();
                var dbcards = CardManager.GetAllCards();
                foreach (var card in dbcards)
                {
                    Card c = new Card();
                    c.ID = card.ID;
                    c.IsActive = card.IsActive ?? true;
                    c.Mana = card.ManaCost;
                    c.Attack = card.Attack;
                    c.Name = card.Name ?? "n/a";
                    c.Life = card.Life;
                    c.Flavor = card.FlavorText ?? "n/a";
                    c.Type = CardManager.GetCardTypeById(card.ID_CardType);
                    c.Pic = card.Image;
                    c.ImageMimeType = card.ImageMimeType?? "n/a";

                    Cards.Add(c);
                }
                    var sorted = Cards;
                if (sortValue != null )
                {
                    sorted= SortHelper.FilterCards(Cards,(int)sortValue);
                }
                if (search!=null )
                {
                    sorted = SortHelper.FilterCards(Cards, search);
                }
                
                return View(sorted);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-CardIndex", e);
                return View("Error");
            }

        }
        #endregion

        #region ACTIONRESULT ORDER INDEX
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult OrderIndex(int? sortValue, string search)
        {
            log.Info("AdminController-OrderIndex");

            try
            {
                List<OrderInfo> Orders = new List<OrderInfo>();
                var dborder = ShopManager.AdminGetAllOrders();
                UserInfo user = new UserInfo();
                foreach (var order in dborder)
                {
                    OrderInfo o = new OrderInfo();
                    o.ID = order.ID;
                    o.isActive = order.IsActive ?? true;
                    o.TotalCost = order.TotalCost ?? 0;
                    o.NumberOfPackages = order.NumberOfPackagesBought??0;
                    o.Date = order.OrderDateTime ?? DateTime.MinValue;
                    o.CreditCard = order.KindOfPayment ?? "n/a";
                    user.Email = order.User.Email ?? "n/a";
                    user.Firstname = order.User.FirstName ?? "n/a";
                    user.Gamertag = order.User.GamerTag ?? "n/a";
                    user.Lastname = order.User.LastName ?? "n/a";
                    o.User = user;
                    Orders.Add(o);
                }
                var sorted = Orders;
                if (sortValue != null)
                {
                    sorted = SortHelper.FilterOrders(Orders, (int)sortValue);
                }
                if (search != null)
                {
                    sorted = SortHelper.FilterOrders(Orders, search);
                }
                return View(sorted);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-OrderIndex", e);
                return View("Error");
            }

        }
        #endregion



        #region VIEWRESULT A_USER EDIT
        /// <summary>
        /// Takes the UserId, gets the User
        /// from DB,returns a ViewResult with user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ViewResult A_UserEdit(int id)
        {
            log.Info("AdminController-AUSERE");
            try
            {
                AdminUserInfo u = new AdminUserInfo();
                var user = UserManager.Get_UserById(id);
                u.ID = user.ID;
                u.IsActive = user.IsActive ?? true;
                u.Firstname = user.FirstName ?? "n/a";
                u.Lastname = user.LastName ?? "n/a";
                u.Gamertag = user.GamerTag ?? "n/a";
                u.Email = user.Email ?? "n/a";
                u.CurrencyBalance = user.AmountMoney ?? 0;
                u.userrole = user.UserRole ?? "n/a";
                u.Pic = user.Avatar;
                u.ImageMimeType = user.AvatarMimeType ?? "n/a";
                u.BanDate = user.BanDate ?? DateTime.MinValue;
                u.EntryDate = user.EntryDate ?? DateTime.MinValue;
                return View(u);
            }
            catch (Exception e)
            {

                Debugger.Break();
                log.Error("AdminController-A_UserEdit", e);
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
                pack.Pic = dbpack.Image;
                pack.ImageMimeType = dbpack.ImageMimeType;
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

        #region VIEWRESULT CARD Edit
        /// <summary>
        /// Takes the CardId, gets the Cardpack
        /// from DB,returns a ViewResult with pack
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ViewResult CardEdit(int id)
        {
            log.Info("AdminController-CardEdit");
            try
            {
                Card c = new Card();
                var card = CardManager.GetCardById(id);
                c.ID = card.ID;
                c.IsActive = card.IsActive ?? true;
                c.Mana = card.ManaCost;
                c.Attack = card.Attack;
                c.Name = card.Name ?? "n/a";
                c.Life = card.Life;
                c.Flavor = card.FlavorText ?? "n/a";
                c.Type = CardManager.GetCardTypeById(card.ID_CardType);
                c.Pic = card.Image;
                c.ImageMimeType = card.ImageMimeType;
                return View(c);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-CardEdit", e);
                return View("Error");
            }

        }
        #endregion

        #region VIEWRESULT ORDER Edit
        /// <summary>
        /// Takes the ORDERId, gets the ORDER
        /// from DB,returns a ViewResult with pack
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ViewResult OrderEdit(int id)
        {
            log.Info("AdminController-OrderEdit");
            try
            {
                OrderInfo o = new OrderInfo();
                UserInfo user = new UserInfo();
                var order = ShopManager.GetOrderById(id);
                o.ID = order.ID;
                o.isActive = order.IsActive ?? true;
                o.TotalCost = order.TotalCost ?? 0;
                o.NumberOfPackages = order.NumberOfPackagesBought ?? 0;
                o.Date = order.OrderDateTime ?? DateTime.MinValue;
                o.CreditCard = order.KindOfPayment ?? "n/a";
                user.Email = order.User.Email ?? "n/a";
                user.Firstname = order.User.FirstName ?? "n/a";
                user.Gamertag = order.User.GamerTag ?? "n/a";
                user.Lastname = order.User.LastName ?? "n/a";
                o.User = user;
                return View(o);
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-OrderEdit", e);
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

        #region ACTIONRESULT CARD EDIT II
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult CardEdit(Card c, HttpPostedFileBase img = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        c.ImageMimeType = img.ContentType;
                        c.Pic = new byte[img.ContentLength];
                        img.InputStream.Read(c.Pic, 0, img.ContentLength);
                    }

                    var type = CardManager.GetTypeByName(c.Type);
                    CardManager.SaveCard(c.ID,c.Name,c.Mana,c.Flavor,c.Attack,c.Life,c.IsActive,type,c.Pic,c.ImageMimeType );
                    TempData["message"] = string.Format("{0} wurde gespeichert", c.Name);
                    return RedirectToAction("CardIndex");
                }
                else
                {
                    return View(c);
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-CardEditII", e);
                return View("Error");
            }
        }
        #endregion

        #region ACTIONRESULT ORDER EDIT II
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult OrderEdit(OrderInfo o)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var d= UserManager.Get_UserByEmail(o.User.Email);
                    int b = d.ID;
                    ShopManager.SaveOrder(b ,0,o.TotalCost,o.NumberOfPackages,o.CreditCard,o.isActive);
                    TempData["message"] = string.Format("{0} wurde gespeichert", o.ID.ToString());
                    return RedirectToAction("OrderIndex");
                }
                else
                {
                    return View(o);
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("AdminController-OrderEditII", e);
                return View("Error");
            }
        }
        #endregion

        #region ACTIONRESULT A_USER EDIT II
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult A_UserEdit(AdminUserInfo au, HttpPostedFileBase img = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        au.ImageMimeType = img.ContentType;
                        au.Pic = new byte[img.ContentLength];
                        img.InputStream.Read(au.Pic, 0, img.ContentLength);
                    }
                    UserManager.SaveAUser(au.ID,
                                             au.Firstname,
                                             au.Lastname, 
                                             au.Email, 
                                             au.Gamertag, 
                                             au.IsActive, 
                                             au.Pic, 
                                             au.ImageMimeType,
                                             au.CurrencyBalance,
                                             au.userrole
                                             );
                    TempData["message"] = string.Format("{0} wurde gespeichert", au.Lastname);
                    return RedirectToAction("A_UserIndex");
                }
                else
                {
                    return View(au);
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

        #region VIEWRESULT CARD CREATE
        public ViewResult CardCreate()
        {
            return View("CardEdit", new Card());
        }
        #endregion

        #region VIEWRESULT ORDER CREATE
        public ViewResult OrderCreate()
        {
            return View("OrderEdit", new OrderInfo());
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

        #region ACTIONRESULT SET CARD INACTIVE
        [HttpPost]
        public ActionResult SetCardInActive(int id)
        {
            var ok = CardManager.SetCardInActive(id);
            var card = CardManager.GetCardById(id);
            if (ok == true)
            {
                TempData["message"] = string.Format("{0} wurde inaktiv gesetzt", card.Name);
            }
            return RedirectToAction("CardIndex");

        }
        #endregion

        #region ACTIONRESULT SET ORDER INACTIVE
        [HttpPost]
        public ActionResult SetOrderInActive(int id)
        {
            var ok = ShopManager.SetOrderInActive(id);
            
            if (ok == true)
            {
                TempData["message"] = string.Format("Bestellung Nr.{0} wurde inaktiv gesetzt", id.ToString());
            }
            return RedirectToAction("OrderIndex");

        }
        #endregion

        #region ACTIONRESULT SET USER INACTIVE
        [HttpPost]
        public ActionResult SetUserInActive(int id)
        {
            var ok = UserManager.SetUserInActive(id);

            if (ok == true)
            {
                TempData["message"] = string.Format("User Nr.{0} wurde inaktiv gesetzt", id);
            }
            return RedirectToAction("A_UserIndex");

        }
        #endregion




        #region FILECONTENTRESULT GET IMAGE

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

        #endregion

        #region FILECONTENTRESULT CARD GET IMAGE
        public FileContentResult CardGetImage(int id)
        {
            var card = CardManager.GetCardById(id);
            if (card != null)
            {
                return File(card.Image, card.ImageMimeType);
            }
            else
            {
                return null;
            }
        } 
        #endregion
    }
}