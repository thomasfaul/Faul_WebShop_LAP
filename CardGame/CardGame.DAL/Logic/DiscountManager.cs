using CardGame.DAL.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CardGame.DAL.Logic
{
    public class DiscountManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        #region GET ALL PACKS
        /// <summary>
        /// Gets all Discounts from the Database
        /// </summary>
        /// <returns></returns> returns a tblpack
        public static List<Discount> GetAllDiscounts()
        {
            try
            {
                log.Info("Discountmanager-GetAllDiscounts");
                List<Discount> ReturnList = null;
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    ReturnList = db.AllDiscounts.Include(c => c.Pack).ToList();
                }
                return ReturnList;
            }
            catch (System.Exception e)
            {
                Debugger.Break();
                log.Error("Discountmanager-GetAllDiscounts",e);
                return null;
            }
        }
        #endregion
    }
}
