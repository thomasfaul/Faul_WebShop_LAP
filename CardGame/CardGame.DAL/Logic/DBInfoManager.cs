using System.Linq;
using CardGame.DAL.Model;
using log4net;
using System.Collections;
using System.Diagnostics;

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
            try
            {

                using (var db = new itin21_ClonestoneFSEntities())
                {
                    numUsers = db.AllUsers.Count();
                }
                log.Info("DB-InfoManager-GetNumUsers " + numUsers);
               
                return numUsers;
            }
            catch (System.Exception e)
            {
                log.Error("Usermanager-GetTotalCost", e);
                return 0;
            }
        }
        #endregion

        #region GET NUM CARDS(Count the Cards)
        /// <summary>
        /// Counts the Cards and gives the Number of Cards
        /// </summary>
        /// <returns></returns>
        public static int GetNumCards()
        {
            try
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
            catch (System.Exception e)
            {
                log.Error("Usermanager-GetTotalCost", e);
                return 0;  
            }
        }
        #endregion

        #region GET NUM DECKS (Count the decks)
        /// <summary>
        /// Counts the Number of the decks and gives back the Number of Decks
        /// </summary>
        /// <returns></returns>
        public static int GetNumDecks()
        {
            try
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
            catch (System.Exception e)
            {
                log.Error("Usermanager-GetTotalCost", e);
                Debugger.Break();
                return 0;
            }
        }
        #endregion

        #region Get Top Ten Buyers
        /// <summary>
        /// Calls the pTop10Buyers stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetTop10Buyers()
        {
            try
            {
                log.Info("DB-InfoManager-Top 10 Buyers");
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
                    log.Info("DB-InfoManager-Top 10 Buyers worked");
                    return cmodel;
                }
            }

            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            }
        }
        #endregion

        #region GET TOP TEN SELLERS
        /// <summary>
        /// Calls the pTopTenSellers stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetTop10Sellers()
        {
            try
            {
                log.Info("DB-InfoManager-Top 10 Sellers");
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
                    log.Info("DB-InfoManager-Top 10 Sellers worked");
                    return cmodel;
                }
            }
            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            }
        }
        #endregion

        #region GET COUNT SIGN UPS WEEK
        public static ChartModel GetCountSignUpsWeek()
        {
            try
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
            catch (System.Exception e)
            {

                Debugger.Break();
                throw e;
            }
        }
        #endregion

        #region GET PURCHASE SUM DAY
        /// <summary>
        /// Calls the pGetSellingstatsDay stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetPurchaseSumDay()
        {
            log.Info("DB-InfoManager-GetPurchaseSumDay");
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
                log.Info("DB-InfoManager-GetPurchaseSumDay worked");
                return cmodel;
            }

        }
        #endregion

        #region GET PURCHASE COUNT DAY
        /// <summary>
        /// Calls the pGetSellingstatsDay stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns></returns>
        public static ChartModel GetPurchaseCountDay()
        {
            log.Info("DB-InfoManager-GetPurchaseCountDay");
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
                log.Info("DB-InfoManager-Top 10 Sellers worked");
                return cmodel;
            }

        }
        #endregion

        #region GET PURCHASES COUNT WEEK
        public static ChartModel GetPurchasesCountWeek()
        {
            try
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
            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            }

        }
        #endregion

        #region GET PURCHASES SUM WEEK
        /// <summary>
        /// Calls the pGetSellingstatsMonth stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetPurchasesSumWeek()
        {
            try
            {
                log.Info("DB-InfoManager-PurchasesSumWeek");
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
                    log.Info("DB-InfoManager-PurchaseSumWeek worked");
                    return cmodel;
                }
            }
            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            }
        }
        #endregion

        #region GET USER SIGN UPS WEEK EMAIL
        /// <summary>
        /// Calls the pGetSellingstatsMonth stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetUserSignUpsWeekEmail()
        {

            try
            {
                log.Info("DB-InfoManager-PurchasesSumWeek");
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
                    log.Info("DB-InfoManager-PurchaseSumWeek worked");
                    return cmodel;
                }
            }
            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            }
        }
        #endregion

        #region GET SELLING WHOLE ORDERS LAST TWO WEEKS
        /// <summary>
        /// Calls the pGetSellingWholeOrdersLastTwoWeeks stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetSellingWohleOrdersLastTwoWeeks()
        {
            try
            {
                log.Info("DB-InfoManager-GetSellingWohleOrdersLastTwoWeeks");
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
                    log.Info("DB-InfoManager-GetSellingWohleOrdersLastTwoWeeks worked");
                    return cmodel;
                }
            }
            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            }
        }
        #endregion

        #region GET SELLING LAST 21 HOURS SUM
        /// <summary>
        /// Calls the pGetSellingWholeOrdersLastTwoWeeks stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetSellingStatsLast21HoursSum()
        {
            log.Info("DB-InfoManager-pGetSellingstatsLast24hours");
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
                log.Info("DB-InfoManager-GetSellingstatsLast24hours worked");
                return cmodel;
            }
        } 
        #endregion

    }
    #region CHARTMODEL
    public class ChartModel
    {

        public ArrayList Values { get; set; }
        public ArrayList Strings { get; set; }
    } 
    #endregion
}
