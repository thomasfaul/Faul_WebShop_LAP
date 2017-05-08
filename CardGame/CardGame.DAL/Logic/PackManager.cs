using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using log4net;
using System.Data.Entity;

namespace CardGame.DAL.Logic
{
    public class PackManager
    //TODO-- könnte durch Vererbung(Schnittstelle) entfernt werden
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly Dictionary<int, string> Packs;

        #region Construktor Packmanager
        /// <summary>
        /// Creates the Constructor
        /// </summary>
        static PackManager()
        {
            log.Info("PackManager-Constructor");
            Packs = new Dictionary<int, string>();
            List<Pack> packList = null;

            using (var db = new itin21_ClonestoneFSEntities())
            {
                packList = db.AllPacks.Where(p=>p.IsActive==true).ToList();
            }

            foreach (var pack in packList)
            {
                Packs.Add(pack.ID, pack.Name);
            }

            Packs.Add(0, "n/a");
        }
        #endregion

        #region GET ALL PACKS
        /// <summary>
        /// Gets all Packs from the Database
        /// </summary>
        /// <returns></returns> returns a tblpack
        public static List<Pack> GetAllPacks()
        {
            log.Info("Usermanager-GetAllPacks");
            List<Pack> ReturnList = null;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                ReturnList = db.AllPacks.Where(p => p.IsActive == true).ToList();
            }
            return ReturnList;
        }
        #endregion

        #region GET PACK BYID
        /// <summary>
        /// Gets a Pack by Pack id and retuns a tblPack
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Pack GetPackById(int id)
        {
            log.Info("Usermanager-GetPackById");
            Pack pack = null;

            using (var db = new itin21_ClonestoneFSEntities())
            {
                //Extention Method
                pack = db.AllPacks.Where(c => c.ID == id).FirstOrDefault();
            }
            return pack;
        }
        #endregion

        #region ADMIN: GET ALL PACKS
        /// <summary>
        /// Gets all Packs from the Database
        /// </summary>
        /// <returns></returns> returns a tblpack
        public static List<Pack> AdminGetAllPacks()
        {
            log.Info("Usermanager-GetAllPacks");
            List<Pack> ReturnList = null;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                ReturnList = db.AllPacks.ToList();
            }
            return ReturnList;
        }
        #endregion

        #region SAVECARDPACK
        /// <summary>
        /// Saves a new Cardpack 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="flavortext"></param>
        /// <param name="ismoney"></param>
        /// <param name="worth"></param>
        /// <param name="numberofcards"></param>
        /// <param name="isactive"></param>
        /// <param name="pic"></param>
        /// <param name="mimetypename"></param>
        public static void SaveCardPack(int id, string name, string flavortext, bool ismoney, int worth, int numberofcards, bool isactive, byte[] pic, string mimetypename)
        {
            if (id == 0)
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    Pack dbpack = new Pack();
                    dbpack.Name = name;
                    dbpack.IsActive = true;
                    dbpack.IsMoney = ismoney;
                    dbpack.NumberOfCards = numberofcards;
                    dbpack.Worth = worth;
                    dbpack.FlavorText = flavortext;
                    dbpack.Image = pic;
                    dbpack.ImageMimeType = mimetypename;
                    db.AllPacks.Add(dbpack);

                    db.SaveChanges();

                }

            }
            else
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    if (db.AllPacks != null)
                    {
                        Pack dbpack = db.AllPacks.SingleOrDefault(p => p.ID == id);

                        dbpack.Name = name;
                        dbpack.IsActive = true;
                        dbpack.IsMoney = ismoney;
                        dbpack.NumberOfCards = numberofcards;
                        dbpack.Worth = worth;
                        dbpack.FlavorText = flavortext;
                        dbpack.Image = pic;
                        dbpack.ImageMimeType = mimetypename;
                        db.Entry(dbpack).State = EntityState.Modified;

                        db.SaveChanges();

                    }


                }
            }
        }
        #endregion

      


        #region SET PACK INACTIVE
        /// <summary>
        /// Sets a pack inactive
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool SetPackInactive(int id)
        {
            using (var db = new itin21_ClonestoneFSEntities())
            {
                Pack dbpack = db.AllPacks.SingleOrDefault(p => p.ID == id);
                dbpack.IsActive = false;

                db.Entry(dbpack).State = EntityState.Modified;
                db.SaveChanges();
            }
            return true; 
            #endregion
        }
    }
}
