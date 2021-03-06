﻿using CardGame.DAL.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;


namespace CardGame.DAL.Logic
{
    public class ShopManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region GET ALL CARD PACKS
        /// <summary>
        /// Returns al List of all Packs
        /// </summary>
        /// <returns></returns>
        public static List<Pack> Get_AllCardPacks()
        {
            log.Info("Usermanager-Get_AllCardPacks");
            var allCardPacks = new List<Pack>();

            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    allCardPacks = db.AllPacks.ToList();
                }
                if (allCardPacks == null)
                    log.Info("Usermanager-Get_AllCardPacks, no CardPacks found");
                    throw new Exception("NoCardPacksFound");
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-Get_AllCardPacks",e);
            }
            return allCardPacks;
        }
        #endregion

        #region GET CARDPACK BY ID
        /// <summary>
        /// Takes the packid and returns the Cardpack
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Pack Get_CardPackById(int id)
        {
            log.Info("Usermanager-Get_CardPackById");
            var dbCardPack = new Pack();

            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    dbCardPack = db.AllPacks.Find(id);
                }
                if (dbCardPack == null)
                    log.Info("Get_CardPackById, no CardPacks found");
                    throw new Exception("CardPackNotFound");
            }
            catch (Exception e)
            {
                //Debugger.Break();
                log.Error("Usermanager-Get_CardPackById", e);
            }
            return dbCardPack;
        } 
        #endregion

        #region GET TOTAL COST
        /// <summary>
        /// Takes the id of the pack and the number of the !!CARDPacks
        /// returns the total cost
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numPacks"></param>
        /// <returns></returns>
        public static decimal GetTotalCost(int id, int numPacks)
        {
            log.Info("Usermanager-GetTotalCost");
            decimal num = 0;
            decimal price = 0;
            bool cur = false;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var pack = db.AllPacks.Find(id);
                    if (pack == null)
                    {
                        log.Error("Usermanager-GetTotalCost,Pack not found");
                        throw new Exception("PackNotFound");
                    }
                    price = pack.Price ?? 0;
                    cur = pack.IsMoney ?? false;

                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-GetTotalCost", e);
            }
            num = Convert.ToDecimal(numPacks);
            return (price * numPacks);
        }
        #endregion


        #region GET TOTAL COSTII
        /// <summary>
        /// Takes the id of the pack and the number of the COINPacks
        /// returns the total cost
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numPacks"></param>
        ///  <param name="quantity"></param>
        /// <returns></returns>
        public static decimal GetTotalCost(int id, int numPacks,int quantity)
        {
            log.Info("Usermanager-GetTotalCost");
            decimal price = 0;
            bool cur = false;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var pack = db.AllPacks.Find(id);
                    if (pack == null)
                    {
                        log.Error("Usermanager-GetTotalCost,Pack not found");
                        throw new Exception("PackNotFound");
                    }
                    price = pack.Price ?? 0;
                    cur = pack.IsMoney ?? false;

                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-GetTotalCost", e);
            }
            return (price * quantity);
        }
        #endregion


        #region ORDER
        /// <summary>
        /// Takes the Id and the nummber of CardPacks
        /// Generates the Cards and returns the new List of Cards
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numPacks"></param>
        /// <returns></returns>
        public static List<Card> Order(int id, int numPacks)
        {
            log.Info("UserManager-Order");
            //3 Steps: Generate Cards, Enter into DB
            Random rng = new Random();
            var generatedCards = new List<Card>();

            //Generate Cards
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var cardPack = db.AllPacks.Find(id);

                    if (cardPack == null)
                    {
                        log.Error("Usermanager-Order, CardPack not found");
                        throw new Exception("CardPackNotFound");
                    }
                    int numCardsToGenerate = cardPack.NumberOfCards ?? 0;

                    numCardsToGenerate *= numPacks;

                    var validIDs = db.AllCards.Select(c => c.ID).ToList();

                    log.Info("validID: " + validIDs.Count.ToString());

                    if (validIDs.Count == 0)
                    {
                        log.Error("Usermanager-Order, No Card IDs Found");
                        throw new Exception("No Card IDs Found");
                    }

                    for (int i = 0; i < numCardsToGenerate; ++i)
                    {
                        int indexID = rng.Next(0, validIDs.Count - 1);
                        int generatedCardID = validIDs[indexID];
                        var generatedCard = db.AllCards.Where(c => c.ID == generatedCardID)
                            .Include(c => c.CardType)
                            .Include(c=>c.CardClass).FirstOrDefault();

                        if (generatedCard == null)
                        {
                            log.Error("Usermanager-Order, Card not found");
                            throw new Exception("CardNotFound");
                        }
                        generatedCards.Add(generatedCard);
                    }
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-Order",e);
            }

            foreach (var c in generatedCards)
            {
                log.Info("UserManager-Order,Card: " + c.ID);
            }

            return generatedCards;
        }
        #endregion

        #region SAVE ORDER
        public static bool SaveOrder(int userid, int cardpackid, int totalsum, int numberofpacks,bool isCurrency)
        {
            log.Info("SaveOrder");
            User person = null;
            Pack cardpack = null;
                string money = "***CardPack***";

            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    person = db.AllUsers.Find(userid);
                    cardpack = db.AllPacks.Find(cardpackid);
                    Purchase order = new Purchase();
                    order.OrderDateTime = DateTime.Now;
                    order.User = person;
                    order.IsActive = true;
                    order.CardPack = cardpack;
                    order.NumberOfPackagesBought = numberofpacks;
                    order.TotalCost = totalsum;
                    order.KindOfPayment = money; 
                    db.AllPurchases.Add(order);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-SaveOrder", e);
                return false;
            }

        }
        #endregion
        #region SAVE ORDERII
        public static bool SaveOrder(int userid, int cardpackid, int totalsum, int numberofpacks, bool iscurrency,string creditcard)
        {
            log.Info("SaveOrderII");
            User person = null;
            Pack cardpack = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    person = db.AllUsers.Find(userid);
                    cardpack = db.AllPacks.Find(cardpackid);
                    Purchase order = new Purchase();
                    order.OrderDateTime = DateTime.Now;
                    order.User = person;
                    order.IsActive = true;
                    order.KindOfPayment = creditcard;
                    order.CardPack = cardpack;
                    order.NumberOfPackagesBought = numberofpacks;
                    order.TotalCost = totalsum;
                    db.AllPurchases.Add(order);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-SaveOrderII", e);
                return false;
            }

        }
        #endregion
        #region SAVE ORDER III
        public static bool SaveOrder(int userid, int cardpackid, int totalsum, int numberofpacks,string creditcard,bool isactive)
        {
            log.Info("SaveOrderIII");
            User person = null;
            Pack cardpack = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    person = db.AllUsers.Find(userid);
                    cardpack = db.AllPacks.Find(cardpackid);
                    Purchase order = new Purchase();
                    order.OrderDateTime = DateTime.Now;
                    order.User = person;
                    order.IsActive = isactive;
                    order.CardPack = cardpack;
                    order.NumberOfPackagesBought = numberofpacks;
                    order.TotalCost = totalsum;
                    db.AllPurchases.Add(order);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-SaveOrder", e);
                return false;
            }

        }
        #endregion
        #region SAVE ORDER IV
        public static bool SaveOrder( int orderid, int totalsum, int numberofpacks, string creditcard, bool isactive,DateTime orderdate)
        {
            log.Info("SaveOrder");
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                   
                    
                    Purchase order = db.AllPurchases.SingleOrDefault(p => p.ID == orderid);
                    order.KindOfPayment= creditcard;
                    order.NumberOfPackagesBought = numberofpacks;
                    order.TotalCost = totalsum;
                    order.OrderDateTime = orderdate;
                    order.IsActive = isactive;
                 
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-SaveOrderIV", e);
                return false;
            }

        }
        #endregion


        #region ADMIN: GET ALL Orders
        /// <summary>
        /// Gets all Packs from the Database
        /// </summary>
        /// <returns></returns> returns a tblpack
        public static List<Purchase> AdminGetAllOrders()
        {
            log.Info("Usermanager-GetAllPacks");
            List<Purchase> Return = null;

            try
            {
            using (var db = new itin21_ClonestoneFSEntities())
            {
                Return = db.AllPurchases.Include(c=>c.User).ToList();
            }
            return Return;
            }
            catch (Exception e)
            {

                Debugger.Break();
                log.Error("Usermanager-SaveOrder", e);
                return Return;
            }

        }
        #endregion

        #region SET ORDER INACTIVE
        /// <summary>
        /// Sets a card inactive
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool SetOrderInActive(int id)
        {
            using (var db = new itin21_ClonestoneFSEntities())
            {
                Purchase dbpurchase = db.AllPurchases.SingleOrDefault(p => p.ID == id);
                dbpurchase.IsActive = false;

                db.Entry(dbpurchase).State = EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }
            #endregion

        #region GET Order BY ID
            /// <summary>
            /// Takes the card Id and returns the associated card
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        public static Purchase GetOrderById(int id)
        {
            log.Info("CardManager-GetOrderById");
            Purchase purch = null;

            using (var db = new itin21_ClonestoneFSEntities())
            {
                purch = db.AllPurchases.Where(c => c.ID == id).Include(c=>c.User).FirstOrDefault();
            }
            return purch;
        }
        #endregion



    }

}

