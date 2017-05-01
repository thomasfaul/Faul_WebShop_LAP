using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using System.Diagnostics;
using log4net;

namespace CardGame.DAL.Logic
{
    public class DeckManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Add Default Decks By UserId
        /// <summary>
        /// Gets the int id and adds the default deck , returns a bool
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool AddDefaultDecksByUserId(int id)
        {
            log.Info("Deckmanager-Add Default Decks By UserId");
            User person = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    person = db.AllUsers.Find(id);
                    bool addedAll = false;
                    if (person == null)
                    { 
                        log.Error("Deckmanager-Add Default Decks By UserId, did not find User");
                        throw new Exception("Did not find User");
                    }  
                    for (int i = 1; i <= 3; ++i)
                    {
                        addedAll = AddDeckByUserId(id, person.Email.Substring(0, 9) + i.ToString());
                    }
                    return addedAll;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Deckmanager-Add Default Decks By UserId", e);
                return false;
            }
        } 
        #endregion

        #region Add Deck by UserId
        /// <summary>
        /// Takes a id of a user and the string of the Deckname
        /// it returns the bool if it worked out
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool AddDeckByUserId(int id, string name)
        {
            log.Info("Deckmanager-AddDeckByUserId");
            User person = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    person = db.AllUsers.Find(id);
                    Deck deck = new Deck();
                    deck.Name = name;
                    deck.User = person;

                    db.AllDecks.Add(deck);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Deckmanager-AddDeckByUserId", e);
                return false;
            }
        } 
        #endregion

        #region Update Deck by Id
        /// <summary>
        /// Takes the List of Deck(Pack)Cards and returns a bool
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deckCards"></param>
        /// <returns></returns>
        public static bool UpdateDeckById(int id, List<DeckCard> deckCards)
        {
            log.Info("Deckmanager-UpdateDeckById");
            Deck deck = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    deck = db.AllDecks.Find(id);
                    if (deck == null)
                    {
                        log.Error("Deckmanager-UpdateDeckById, Deck nicht gefunden");
                        throw new Exception("Deck nicht gefunden");

                    }

                    var existingDeckList = deck.AllDeckCards.ToList();

                    foreach (var dc in existingDeckList)
                    {
                        deck.AllDeckCards.Remove(dc);
                    }
                    db.SaveChanges();

                    foreach (var dc in deckCards)
                    {
                        var dbDeckCard = new DeckCard();
                        dbDeckCard.NumberOfCards = dc.NumberOfCards ?? 0;
                        dbDeckCard.Deck = db.AllDecks.Find(id);
                        dbDeckCard.Card = db.AllCards.Find(dc.Card.ID);
                        db.AllDeckCards.Add(dbDeckCard);
                    }

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Deckmanager-UpdateDeckById",e);
                return false;
            }
        } 
        #endregion

        #region Get Deck Cards By Id
        /// <summary>
        /// Takes the id of the deck and returns
        /// a list of Deckcards
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Card> GetDeckCardsById(int id)
        {
            log.Info("Deckmanager-GetDeckCardsById");
            var deckCards = new List<Card>();

            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var dbDeck = db.AllDecks.Where(d => d.ID == id).FirstOrDefault();
                    if (dbDeck == null)
                    {
                        log.Error("Deckmanager-GetDeckCardsById, Deck does not exist");
                        throw new Exception("Deck does not exist");
                    }
                    var dbDeckCollection = dbDeck.AllDeckCards.ToList();
                    if (dbDeckCollection == null)
                    {
                        log.Error("Deckmanager-GetDeckCardsById, Card Collection not found");
                        throw new Exception("Card Collection Not Found");
                    }
                    foreach (var dc in dbDeckCollection)
                    {
                        for (int i = 0; i < dc.NumberOfCards; i++)
                        deckCards.Add(dc.Card);
                    }

                    return deckCards;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Deckmanager-GetDeckCardsById", e);
                return null;
            }
        } 
        #endregion

        #region Get Numbers of Deck Cards by Id
        /// <summary>
        /// Takes the id and gives back the number of Deckcards
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetNumbersOfDeckCardsById(int id)
        {
            log.Info("Deckmanager-GetNumbersOfDeckCardsById");
            Deck deck = null;
            int numCards = -1;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    deck = db.AllDecks.Find(id);
                    if (deck == null)
                    {
                        log.Error("Deckmanager-GetNumbersOfDeckCardsById, Deck not found");
                        throw new Exception("DeckNotFound");
                    }
                    numCards = deck.AllDeckCards.Count;

                    return numCards;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Deckmanager-GetNumbersOfDeckCardsById",e);
                return numCards;
            }
        } 
        #endregion

        #region Get Deck By Id (Not used)
        /// <summary>
        /// Gets a Deck by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Deck GetDeckById(int id)
        {
            log.Info("Deckmanager-GetDeckById");
            Deck deck = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    deck = db.AllDecks.Find(id);
                    if (deck == null)
                    {
                        log.Error("Deckmanager-GetDeckById, Deck not found");
                        throw new Exception("DeckNotFound");
                    }
                    return deck;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Deckmanager-GetDeckById, Deck not found",e);
                return null;
            }
        } 
        #endregion

    }
}
