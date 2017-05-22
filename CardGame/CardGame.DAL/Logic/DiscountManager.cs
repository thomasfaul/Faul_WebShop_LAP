using CardGame.DAL.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Diagnostics;

namespace CardGame.DAL.Logic
{
    public class DiscountManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Pack> GetAllPacks()
        {
            try
            {
                log.Info("Discountmanager-GetAllPacks");
                List<Pack> ReturnList = null;
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    ReturnList = db.AllPacks.Where(p => p.IsActive == true).ToList();
                }
                return ReturnList;
            }
            catch (System.Exception e)
            {
                Debugger.Break();
                log.Error("Discountmanager-GetAllPacks", e);
                return null;
            }
        }
        public static List<Discount> GetDiscountbyId(int id)
        {
            try
            {
                log.Info("Discountmanager-GetDiscountbyId");
                List<Discount> ReturnList = null;
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    ReturnList = db.AllDiscounts.Where(d => d.ID == id).ToList();
                }
                return ReturnList;
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Discountmanager-GetDiscountbyId", e);
                return null;
            }
            
        }
        public static List<Discount> GetAllDiscount()
        {
            try
            {
                log.Info("Discountmanager-GetAllDiscounts");
                List<Discount> ReturnList = null;
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    ReturnList = db.AllDiscounts.ToList();
                }
                return ReturnList;
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Discountmanager-GetAllDiscounts", e);
                return null;
            }


        }



    }
}
