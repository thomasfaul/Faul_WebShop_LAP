using System.Linq;
using CardGame.DAL.Model;
using log4net;
using System.Collections.Generic;
using System;
using System.Data.Entity.Core.Objects;

namespace CardGame.DAL.Logic
{
    public class DBInfoManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region GET NUM USERS (Count the Users)
        /// <summary>
        /// Counts all USERS and gives the number back
        /// </summary>
        /// <returns></returns>
        public static int GetNumUsers()
        {
            log.Info("DB-InfoManager-GetNumUsers");
            int numUsers = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                numUsers = db.AllUsers.Count();
            }
            log.Info("DB-InfoManager-GetNumUsers " + numUsers);
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
            log.Info("DB-InfoManager-GetNumCards");
            int numCards = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                numCards = db.AllCards.Count();
            }
            log.Info("DB-InfoManager-GetNumCards " + numCards);

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
            log.Info("DB-InfoManager-GetNumDecks");
            int numDecks = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                numDecks = db.AllDecks.Count();
            }

            log.Info("DB-InfoManager-GetNumDecks " + numDecks);

            return numDecks;
        }
        #endregion

        public static ChartModel GetTop10Buyers()
        {
            ChartModel cmodel = new Logic.ChartModel();
            using (var db=new itin21_ClonestoneFSEntities())
            {
              var result= db.pGetTop10Buyers().ToList();
                foreach (var item in result)
                {
                    cmodel.Info = item.email;
                    cmodel.Wert = item.NumberofPurchases ?? 0;
                }

            }
            return cmodel;
        }
        public static List<pGetTop5Buyers_Result> GetTop10Sellers()
        {
            List<pGetTop5Buyers_Result> result = new List<pGetTop5Buyers_Result>();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                db.pGetTop10Sellers();
            }
            return result;
        }
        public static List<pGetTop5Buyers_Result> GetCountedSignUpsWeek()
        {
            List<pGetTop5Buyers_Result> result = new List<pGetTop5Buyers_Result>();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                db.pGetCountSignUpsWeek();
            }
            return result;
        }
        public static List<pGetTop5Buyers_Result> GetSellingStatsDay()
        {
            List<pGetTop5Buyers_Result> result = new List<pGetTop5Buyers_Result>();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                db.pGetSellingstatsDay();
            }
            return result;
        }
        public static List<pGetTop5Buyers_Result> GetSellingStatsMonth()
        {
            List<pGetTop5Buyers_Result> result = new List<pGetTop5Buyers_Result>();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                db.pGetSellingstatsMonth();
            }
            return result;
        }

    }
    public class ChartModel
    {
      public int Wert { get; set; }
      public string Info { get; set; }

    }
}
