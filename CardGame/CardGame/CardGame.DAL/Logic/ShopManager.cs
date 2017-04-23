using CardGame.DAL.Model;
using CardGame.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace CardGame.DAL.Logic
{
    public class ShopManager
    {
        #region GET ALL CARD PACKS
        /// <summary>
        /// Returns al List of all Packs
        /// </summary>
        /// <returns></returns>
        public static List<tblpack> Get_AllCardPacks()
        {
            var allCardPacks = new List<tblpack>();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    allCardPacks = db.tblpack.ToList();
                }
                if (allCardPacks == null)
                    throw new Exception("NoCardPacksFound");
            }
            catch (Exception e)
            {
                Writer.LogError(e);
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
        public static tblpack Get_CardPackById(int id)
        {
            var dbCardPack = new tblpack();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    dbCardPack = db.tblpack.Find(id);
                }
                if (dbCardPack == null)
                    throw new Exception("CardPackNotFound");
            }
            catch (Exception e)
            {
                Writer.LogError(e);
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
            decimal num = 0;
            decimal price = 0;
            bool cur = false;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var pack = db.tblpack.Find(id);
                    if (pack == null)
                    {
                        throw new Exception("PackNotFound");
                    }
                    price = pack.packprice ?? 0;
                    cur = pack.ismoney ?? false;

                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
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
        public static List<tblcard> Order(int id, int numPacks)
        {
            //3 Steps: Generate Cards, Enter into DB
            Random rng = new Random();
            var generatedCards = new List<tblcard>();

            //Generate Cards
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var cardPack = db.tblpack.Find(id);

                    if (cardPack == null)
                    {
                        throw new Exception("CardPackNotFound");
                    }
                    int numCardsToGenerate = cardPack.numcards ?? 0;

                    numCardsToGenerate *= numPacks;

                    var validIDs = db.tblcard.Select(c => c.idcard).ToList();

                    Writer.LogInfo("vID: " + validIDs.Count.ToString());

                    if (validIDs.Count == 0)
                    {
                        throw new Exception("No Card IDs Found");
                    }

                    for (int i = 0; i < numCardsToGenerate; ++i)
                    {
                        int indexID = rng.Next(0, validIDs.Count - 1);
                        int generatedCardID = validIDs[indexID];
                        var generatedCard = db.tblcard.Where(c => c.idcard == generatedCardID).Include(c => c.tbltype).FirstOrDefault();

                        if (generatedCard == null)
                        {
                            throw new Exception("CardNotFound");
                        }
                        generatedCards.Add(generatedCard);
                    }
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }

            foreach (var c in generatedCards)
            {
                Writer.LogInfo("Card: " + c.idcard);
            }

            return generatedCards;
        }
        #endregion

    }
}
