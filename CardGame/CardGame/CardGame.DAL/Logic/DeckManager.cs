using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Log;
using CardGame.DAL.Model;

namespace CardGame.DAL.Logic
{
    public class DeckManager
    {
        #region Add Default Decks By UserId
        /// <summary>
        /// Gets the int id and adds the default deck , returns a bool
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool AddDefaultDecksByUserId(int id)
        {
            tblperson person = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    person = db.tblperson.Find(id);
                    bool addedAll = false;
                    if (person == null)
                        throw new Exception("Did not find User");

                    for (int i = 1; i <= 3; ++i)
                    {
                        addedAll = AddDeckByUserId(id, person.email.Substring(0, 9) + i.ToString());
                    }
                    return addedAll;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
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
            tblperson person = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    person = db.tblperson.Find(id);
                    tbldeck deck = new tbldeck();
                    deck.deckname = name;
                    deck.tblperson = person;

                    db.tbldeck.Add(deck);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
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
        public static bool UpdateDeckById(int id, List<tbldeckcard> deckCards)
        {
            tbldeck deck = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.tbldeck.Find(id);
                    if (deck == null)
                    {
                        throw new Exception("DeckNotFound");
                    }

                    var existingDeckList = deck.tbldeckcard.ToList();

                    foreach (var dc in existingDeckList)
                    {
                        deck.tbldeckcard.Remove(dc);
                    }
                    db.SaveChanges();

                    foreach (var dc in deckCards)
                    {
                        var dbDeckCard = new tbldeckcard();
                        dbDeckCard.numcards = dc.numcards;
                        dbDeckCard.tbldeck = db.tbldeck.Find(id);
                        dbDeckCard.tblcard = db.tblcard.Find(dc.tblcard.idcard);
                        db.tbldeckcard.Add(dbDeckCard);
                    }

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
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
        public static List<tblcard> GetDeckCardsById(int id)
        {
            var deckCards = new List<tblcard>();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var dbDeck = db.tbldeck.Where(d => d.iddeck == id).FirstOrDefault();
                    if (dbDeck == null)
                    {
                        throw new Exception("Deck does not exist");
                    }
                    var dbDeckCollection = dbDeck.tbldeckcard.ToList();
                    if (dbDeckCollection == null)
                    {
                        throw new Exception("Card Collection Not Found");
                    }
                    foreach (var dc in dbDeckCollection)
                    {
                        for (int i = 0; i < dc.numcards; i++)
                        deckCards.Add(dc.tblcard);
                    }

                    return deckCards;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
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
            tbldeck deck = null;
            int numCards = -1;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.tbldeck.Find(id);
                    if (deck == null)
                    {
                        throw new Exception("DeckNotFound");
                    }
                    numCards = deck.tbldeckcard.Count;

                    return numCards;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
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
        public static tbldeck GetDeckById(int id)
        {
            tbldeck deck = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.tbldeck.Find(id);
                    if (deck == null)
                    {
                        throw new Exception("DeckNotFound");
                    }
                    return deck;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return null;
            }
        } 
        #endregion

    }
}
