using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;

namespace CardGame.DAL.Logic
{
    public class PackManager
    //TODO-- könnte durch Vererbung(Schnittstelle) entfernt werden
    {
        public static readonly Dictionary<int, string> Packs;

        #region Construktor Packmanager
        /// <summary>
        /// Creates the Constructor
        /// </summary>
        static PackManager()
        {
            Packs = new Dictionary<int, string>();
            List<tblpack> packList = null;

            using (var db = new ClonestoneFSEntities())
            {
                packList = db.tblpack.ToList();
            }

            foreach (var pack in packList)
            {
                Packs.Add(pack.idpack, pack.packname);
            }

            Packs.Add(0, "n/a");
        }
        #endregion

        #region GET ALL PACKS
        /// <summary>
        /// Gets all Packs from the Database
        /// </summary>
        /// <returns></returns> returns a tblpack
        public static List<tblpack> GetAllPacks()
        {
            List<tblpack> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                ReturnList = db.tblpack.ToList();
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
        public static tblpack GetPackById(int id)
        {
            tblpack pack = null;

            using (var db = new ClonestoneFSEntities())
            {
                //Extention Method
                pack = db.tblpack.Where(c => c.idpack == id).FirstOrDefault();
            }
            return pack;
        }
        #endregion
    }
}
