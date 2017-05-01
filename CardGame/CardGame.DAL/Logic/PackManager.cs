using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using log4net;

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
                packList = db.AllPacks.ToList();
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
                ReturnList = db.AllPacks.ToList();
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
    }
}
