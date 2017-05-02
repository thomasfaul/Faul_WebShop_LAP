using CardGame.DAL.Model;
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
        /// Takes the id of the pack and the number of the Packs
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
        //public static bool SaveOrder(int userid,int cardpackid,int id)
        //{
        //    using (var db = new itin21_ClonestoneFSEntities())
        //    {
        //        Purchase order = new Purchase();
        //        User user = new User();
                
        //        order.OrderDateTime = DateTime.Now;
        //        order.CardPack = Get_CardPackById(cardpackid);
        //        order.User=

        //        //db.AllPurchases.Add(order);
        //        db.SaveChanges();
        //    }
                


        //    return true;
        //}

    }
}
