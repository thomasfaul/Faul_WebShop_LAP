using System.Linq;
using CardGame.DAL.Model;
using CardGame.Log;

namespace CardGame.DAL.Logic
{
    public class DBInfoManager
    {
        #region GET NUM USERS (Count the Users)
        /// <summary>
        /// Counts all USERS and gives the number back
        /// </summary>
        /// <returns></returns>
        public static int GetNumUsers()
        {
            int numUsers = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                numUsers = db.tblperson.Count();
            }
            Writer.LogInfo("GetNumUsers " + numUsers);
            return numUsers;
        }
        #endregion

        #region GET NUM CARDS(Count the Cards)
        /// <summary>
        /// Counts the Cards and gives the Number back
        /// </summary>
        /// <returns></returns>
        public static int GetNumCards()
        {
            int numCards = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                numCards = db.tblcard.Count();
            }
            Writer.LogInfo("GetNumCards " + numCards);

            return numCards;
        }
        #endregion

        #region GET NUM DECKS (Count the decks)
        /// <summary>
        /// Counts the Number of the decks and gives the Number back
        /// </summary>
        /// <returns></returns>
        public static int GetNumDecks()
        {
            int numDecks = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                numDecks = db.tbldeck.Count();
            }

            Writer.LogInfo("GetNumDecks " + numDecks);

            return numDecks;
        }
        #endregion
    }
}
