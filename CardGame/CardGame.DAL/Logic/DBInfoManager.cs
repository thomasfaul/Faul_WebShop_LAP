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

        #region Get Top 5 Customers/Names
        /// <summary>
        /// Calls the pTop5 Customers stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetTop10CustomersNames()
        {
            try
            {
                log.Info("DB-InfoManager-Top 5 Customers");
                ChartModel cmodel = new ChartModel();
                cmodel.Strings = new ArrayList();
                cmodel.Values = new ArrayList();
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var result = db.pGetTop5Customers().ToList();

                    for (int i = 0; i < result.Count; i++)
                    {
                        cmodel.Strings.Add(result[i].firstname);
                        cmodel.Values.Add(result[i].NumberofPurchases);
                    }
                    log.Info("DB-InfoManager-Top 5 Customers worked");
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

        #region Get Top 5 Customers/Email
        /// <summary>
        /// Calls the pTop5CustomersEmail stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetTop5CustomersEmail()
        {
            try
            {
                log.Info("DB-InfoManager-Top 5 Customers/Email");
                ChartModel cmodel = new ChartModel();
                cmodel.Strings = new ArrayList();
                cmodel.Values = new ArrayList();
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var result = db.pGetTop5CustomersEmails().ToList();

                    for (int i = 0; i < result.Count; i++)
                    {
                        cmodel.Strings.Add(result[i].email);
                        cmodel.Values.Add(result[i].NumberofPurchases);
                    }
                    log.Info("DB-InfoManager-Top 5 Customers/Email worked");
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

        #region Get Top 5 Customers/Email
        /// <summary>
        /// Calls the pTop5CustomersEmail stored Procedure
        /// takes the Values and gives back the ChartModel
        /// </summary>
        /// <returns>ChartModel</returns>
        public static ChartModel GetTop5Sellers()
        {
            try
            {
                log.Info("DB-InfoManager-Top 5 Sellers");
                ChartModel cmodel = new ChartModel();
                cmodel.Strings = new ArrayList();
                cmodel.Values = new ArrayList();
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var result=db.pGetTop5Sellers().ToList();

                    for (int i = 0; i < result.Count; i++)
                    {
                        cmodel.Strings.Add(result[i].packname);
                        cmodel.Values.Add(result[i].NumberofPurchases);
                    }
                    log.Info("DB-InfoManager-Top 5 Sellers");
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

    }
    #region CHARTMODEL
    public class ChartModel
    {

        public ArrayList Values { get; set; }
        public ArrayList Strings { get; set; }
    } 
    #endregion
}
