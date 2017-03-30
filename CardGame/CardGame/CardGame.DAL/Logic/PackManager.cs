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
        public static readonly Dictionary<int, string> PackCards;

        static PackManager()
        {
            PackCards = new Dictionary<int, string>();
            List<tblcard> cardList = null;

            using (var db = new ClonestoneFSEntities())
            {
                cardList = db.tblcard.ToList();
            }

            foreach (var card in cardList)
            {
                PackCards.Add(card.idcard, card.cardname);
            }

            PackCards.Add(0, "n/a");
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
