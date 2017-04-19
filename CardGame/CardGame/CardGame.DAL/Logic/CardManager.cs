using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;


namespace CardGame.DAL.Logic
{

    public class CardManager
    {
        public static readonly Dictionary<int, string> CardTypes;

        #region Constructor CardManager 
        /// <summary>
        ///Design of the Object CardManager wird hier erstellt 
        /// </summary>
        static CardManager()
        {
            CardTypes = new Dictionary<int, string>();
            List<tbltype> cardTypeList = null;


            using (var db = new ClonestoneFSEntities())
            {
                cardTypeList = db.tbltype.ToList();
            }

            foreach (var type in cardTypeList)
            {
                CardTypes.Add(type.idtype, type.typename);
            }

            CardTypes.Add(0, "n/a");
        } 
        #endregion

        #region GET ALL CARDS
        /// <summary>
        /// Returns a list of all cards
        /// </summary>
        /// <returns></returns>
        public static List<tblcard> GetAllCards()
        {
            List<tblcard> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                ReturnList = db.tblcard.ToList();
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
            string TypeName = "n/a";

            using (var db = new ClonestoneFSEntities())
            {
                TypeName = db.tbltype.Find(id).typename;
            }
            return TypeName;
        }
        #endregion

        #region GET CARD BY ID
        /// <summary>
        /// Takes the card Id and returns the associated card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static tblcard GetCardById(int id)
        {
            tblcard card = null;

            using (var db = new ClonestoneFSEntities())
            {
                //Extention Method
                card = db.tblcard.Where(c => c.idcard == id).FirstOrDefault();

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
        public static void GetCardByType(int type)
        {

            using (var db = new ClonestoneFSEntities())
            {

                var ReturnList = db.tblcard.Join(db.tbltype, t => t.idcard, c => c.idtype, (c, types)
                                         => new
                                         {
                                             Cardname = c.cardname,
                                             Mana = c.mana,
                                             Life = c.life,
                                             Attack = c.attack,
                                             Flavor = c.flavor,
                                             Pic = c.pic,
                                             Types = types,
                                             FkTypes = c.fktype
                                         }).Where(t => t.FkTypes == type);
                //return ReturnList;                          
            }
        }
        #endregion
    }
}
