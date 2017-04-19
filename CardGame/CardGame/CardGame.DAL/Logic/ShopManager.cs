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

        public static decimal GetTotalCost(int id, int numPacks)
        {
            decimal num = 0;
           decimal price = 0;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var pack = db.tblpack.Find(id);
                    if (pack == null)
                    {
                        throw new Exception("PackNotFound");
                    }
                    price =pack.packprice ?? 0;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }
            num = Convert.ToDecimal(numPacks);
            return (price * numPacks);
        }

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

            //Cards were successfully generated
            //Now we can enter them into the DB

            return generatedCards;
        }
    }
}
