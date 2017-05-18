using System.Linq;
using CardGame.DAL.Model;
using log4net;
using System.Collections.Generic;
using System;
using System.Data.Entity.Core.Objects;
using System.Collections;

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
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetTop10Buyers().ToList();
                
                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].email);
                    cmodel.Values.Add(result[i].NumberofPurchases);
                }
             return cmodel;
            }

        }
        public static ChartModel GetTop10Sellers()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                //var result = db.pGetTop10Sellers()ToList();

                //for (int i = 0; i < result.Count; i++)
                //{
                //    cmodel.Strings.Add(result[i].email);
                //    cmodel.Values.Add(result[i].NumberofPurchases);
                //}
                return cmodel;
            }

        }
        public static ChartModel GetCountSignUpsWeek()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetCountSignUpsWeek().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Signup);
                    cmodel.Values.Add(result[i].signed);
                }
                return cmodel;
            }

        }
        public static ChartModel GetPurchaseSumDay()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetSellingstatsDay().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Einnahmen);
                    cmodel.Values.Add(result[i].Tag);
                }
                return cmodel;
            }

        }
        public static ChartModel GetPurchaseCountDay()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetSellingstatsDay().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Kaeufe);
                    cmodel.Values.Add(result[i].Tag);
                }
                return cmodel;
            }

        }
        public static ChartModel GetPurchasesCountWeek()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetSellingstatsMonth().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Kaeufe);
                    cmodel.Values.Add(result[i].Woche);
                }
                return cmodel;
            }

        }
        public static ChartModel GetPurchasesSumWeek()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetSellingstatsMonth().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Einnahmen);
                    cmodel.Values.Add(result[i].Woche);
                }
                return cmodel;
            }

        }
        public static ChartModel GetUserSignUpsWeekEmail()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetUserSignUpsWeek().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Signup);
                    cmodel.Values.Add(result[i].email);
                }
                return cmodel;
            }

        }
        public static ChartModel GetSellingWohleOrdersLastTwoWeeks()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetSellingWholeOrdersLastTwoWeeks().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Kaeufe);
                    cmodel.Values.Add(result[i].Tag);
                }
                return cmodel;
            }

        }
        public static ChartModel GetSellingStatsLast21HoursSum()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetSellingstatsLast24hours().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Einnahmen);
                    cmodel.Values.Add(result[i].Stunde);
                }
                return cmodel;
            }

        }
        public static ChartModel GetSellingStatsLast21HoursCount()
        {
            ChartModel cmodel = new ChartModel();
            cmodel.Strings = new ArrayList();
            cmodel.Values = new ArrayList();
            using (var db = new itin21_ClonestoneFSEntities())
            {
                var result = db.pGetSellingstatsLast24hours().ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    cmodel.Strings.Add(result[i].Kaeufe);
                    cmodel.Values.Add(result[i].Stunde);
                }
                return cmodel;
            }

        }
    }
    public class ChartModel
    {

      public ArrayList Values { get; set; }
      public ArrayList Strings { get; set; }
    }
}
