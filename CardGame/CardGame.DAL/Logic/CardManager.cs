using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using log4net;

namespace CardGame.DAL.Logic
{

    public class CardManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly Dictionary<int, string> CardTypes;
        public static readonly Dictionary<int, string> CardClasses;

        #region Constructor CardManager 
        /// <summary>
        ///Design of the Object CardManager wird hier erstellt 
        /// </summary>
        static CardManager()
        {
            log.Info("CardManager-Construktor");
            CardTypes = new Dictionary<int, string>();
            CardClasses = new Dictionary<int, string>();
            List<CardType> cardTypeList = null;
            List<CardClass> cardClassList = null;
            

            using (var db = new itin21_ClonestoneFSEntities())
            {
                cardTypeList = db.AllCardTypes.ToList();
                cardClassList = db.AllCardClasses.ToList();
            }

            foreach (var type in cardTypeList)
            {
                CardTypes.Add(type.ID, type.Name);
            }
            foreach(var clas in cardClassList)
            {
                CardClasses.Add(clas.ID, clas.Name);
            }
            CardClasses.Add(0, "n/a");
            CardTypes.Add(0, "n/a");
        } 
        #endregion

        #region GET ALL CARDS
        /// <summary>
        /// Returns a list of all cards
        /// </summary>
        /// <returns></returns>
        public static List<Card> GetAllCards()
        {
            log.Info("CardManager-GetAllCards");
            List<Card> ReturnList = null;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                ReturnList = db.AllCards.ToList();
            }
            return ReturnList;
        }
        #endregion

        #region GET CARDTYPE BY ID
        /// <summary>
        /// TODO: GET Cardtype by id überprüfen, selbst gemacht in irgendeinem Anfall 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCardTypeById(int? id)
        {
            log.Info("CardManager-GetCardTypeById");
            string TypeName = "n/a";

            using (var db = new itin21_ClonestoneFSEntities())
            {
                TypeName = db.AllCardTypes.Find(id).Name;
            }
            return TypeName;
        }
        #endregion

        #region GET CARDClass BY ID
        /// <summary>
        /// TODO: GET Class by id überprüfen, selbst gemacht in irgendeinem Anfall 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCardClassById(int id)
        {
            log.Info("CardManager-GetCardClassById");
            string ClassName = "n/a";

            using (var db = new itin21_ClonestoneFSEntities())
            {
                ClassName = db.AllCardClasses.Find(id).Name;
            }
            return ClassName;
        }
        #endregion

        #region GET CARD BY ID
        /// <summary>
        /// Takes the card Id and returns the associated card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Card GetCardById(int id)
        {
            log.Info("CardManager-GetCardById");
            Card card = null;

            using (var db = new itin21_ClonestoneFSEntities())
            {
                //Extention Method
                card = db.AllCards.Where(c => c.ID == id).FirstOrDefault();

                //Klassisch LINQ
                //card = (from c in db.tblcard
                //        where c.idcard == id
                //        select c).FirstOrDefault();
            }
            return card;
        }
        #endregion

        #region GET CARD BY TYPE (not ready)
        /// <summary>
        /// TODO: get card by type
        /// Should return a list of cards which have the same type
        /// </summary>
        /// <param name="type"></param>
        //public static void GetCardByType(int type)
        //{

        //    using (var db = new itin21_ClonestoneFSEntities())
        //    {

        //        var ReturnList = db.tblcard.Join(db.tbltype, t => t.idcard, c => c.idtype, (c, types)
        //                                 => new
        //                                 {
        //                                     Cardname = c.cardname,
        //                                     Mana = c.mana,
        //                                     Life = c.life,
        //                                     Attack = c.attack,
        //                                     Flavor = c.flavor,
        //                                     Pic = c.pic,
        //                                     Types = types,
        //                                     FkTypes = c.fktype
        //                                 }).Where(t => t.FkTypes == type);
        //        //return ReturnList;                          
        //    }
        //}
        #endregion
    }
}
