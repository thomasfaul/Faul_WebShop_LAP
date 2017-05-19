using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using log4net;
using System.Data.Entity;
using System.Diagnostics;

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
     
            

            using (var db = new itin21_ClonestoneFSEntities())
            {
                cardTypeList = db.AllCardTypes.ToList();
            
            }

            foreach (var type in cardTypeList)
            {
                CardTypes.Add(type.ID, type.Name);

            }
            
            CardTypes.Add(0, "n/a");
        } 
        #endregion

        #region GET ALL CARDS
        /// <summary>
        /// Returns a list of all cards
        /// </summary>
        /// <returns>List<Card></returns>
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
        public static string GetCardTypeById(int id)
        {
            log.Info("CardManager-GetCardTypeById");
            string typename;

            using (var db = new itin21_ClonestoneFSEntities())
            {
                  
                    var cardtype= db.AllCardTypes.Where(c=>c.ID==id).FirstOrDefault();
                  typename=cardtype.Name;
            }         
            return typename;
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


        #region GET TypeByName
        /// <summary>
        /// Takes the Name of the type and returns the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetTypeByName(string name)
        {
            log.Info("CardManager-GetTypebyName");

            int result=0;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                //Extention Method
                var type = db.AllCardTypes.Where(c => c.Name == name).FirstOrDefault();
                result = type.ID;

            }
            return result;
        }
        #endregion

        #region GET ClassByName
        /// <summary>
        /// Takes the Name of the class and returns the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetClassByName(string name)
        {
            log.Info("CardManager-GetClassbyName");

            int result=0;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                //Extention Method
                var cclass = db.AllCardClasses.Where(c => c.Name == name).FirstOrDefault();
                result = cclass.ID;

            }
            return result;
        }
        #endregion


        #region SAVE CARD
        /// <summary>
        /// Takes the Values of the Card and Saves a new Card
        /// or changes a Card in the Database with new Values
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
        public static void SaveCard(int id, string name, byte mana,string flavortext, short attack, short life,  bool isactive,int cardtype, byte[] pic, string mimetypename)
        {
            log.Info("CardManager-SaveCard");
            if (id == 0)
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    Card dbcard = new Card();
                    dbcard.Name = name;
                    dbcard.IsActive = isactive;
                    dbcard.ManaCost = mana;
                    dbcard.Attack = attack;
                    dbcard.Life = life;
                    dbcard.FlavorText = flavortext;
                    dbcard.Image = pic;
                    dbcard.ImageMimeType = mimetypename;
                    dbcard.ID_CardType = cardtype;

                    db.AllCards.Add(dbcard);

                    db.SaveChanges();
                    log.Info("CardManager-SaveCard, New Card was build");
                }

            }
            else
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    if (db.AllPacks != null)
                    {
                      

                        Card dbcard = db.AllCards.SingleOrDefault(p => p.ID == id); ;
                        dbcard.Name = name;
                        dbcard.IsActive = isactive;
                        dbcard.ManaCost = mana;
                        dbcard.Attack = attack;
                        dbcard.Life = life;
                        dbcard.FlavorText = flavortext;
                        dbcard.Image = pic;
                        dbcard.ImageMimeType = mimetypename;
                        dbcard.ID_CardType = cardtype;



                        db.Entry(dbcard).State = EntityState.Modified;

                        db.SaveChanges();
                        log.Info("CardManager-SaveCard, Card is actual");
                    }
                }
            }
        }
        #endregion

        #region SET CARD INACTIVE
        /// <summary>
        /// Sets a card inactive
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool SetCardInActive(int id)
        {
            log.Info("CardManager-Set Card inactive; ");
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    Card dbcard = db.AllCards.SingleOrDefault(p => p.ID == id);
                    dbcard.IsActive = false;

                    db.Entry(dbcard).State = EntityState.Modified;
                    db.SaveChanges();
                    log.Info("Card was set inactive; ");
                }
                return true;
            }
            catch (System.Exception)
            {
                Debug.WriteLine("Card couln't be set inactive");
                throw;
            }
        }
         #endregion
    }
}
