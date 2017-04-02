using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.DAL.Model;
using PagedList.Mvc;

namespace CardGame.DAL.Logic
{
    public class PackManager
    {
        public static readonly Dictionary<int, string> Packs;

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
        public static List<tblpack> GetAllPacks()
        {
            List<tblpack> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                ReturnList = db.tblpack.ToList();
            }
            return ReturnList;
        }
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

    }

}
