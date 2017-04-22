using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using CardGame.Log;
using System.Data.Entity;

namespace CardGame.DAL.Logic
{
    public class UserManager
    {

        //public static readonly Dictionary<int, string> UserRoleNames;
        public static readonly Dictionary<int, string> CardClassNames;
        public static readonly Dictionary<int, string> CardTypeNames;

        #region Constructor USERMANAGER
        static UserManager()
        {
            //UserRoleNames = new Dictionary<int, string>();
            CardClassNames = new Dictionary<int, string>();
            CardTypeNames = new Dictionary<int, string>();
            //List<tblUserRole> userRoleList = null;
            List<tbltype> cardTypeList = null;
            List<tblclass> cardClassList = null;

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    //cardRoleList = db.tblrole.ToList();
                    cardTypeList = db.tbltype.ToList();
                    cardClassList = db.tblclass.ToList();
                }
                //foreach (var role in userRoleList)
                //{
                //    UserRoleNames.Add(role.idUserrole, role.rolename);
                //}
                foreach (var type in cardTypeList)
                {
                    CardTypeNames.Add(type.idtype, type.typename);
                }
                foreach (var clas in cardClassList)
                {
                    CardTypeNames.Add(clas.idclass, clas.@class);
                }
                //UserRoleNames.Add(0, "n/a");
                CardTypeNames.Add(0, "n/a");
                CardClassNames.Add(0, "n/a");
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }

        }
        #endregion

        #region GetAllUser
        /// <summary>
        /// Connects to the Db and gets all Users
        /// </summary>
        /// <returns></returns>
        public static List<tblperson> GetAllUser()
        {
            List<tblperson> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                // TODO - Include
                // .Include(t => t.tabelle) um einen Join zu machen !
                ReturnList = db.tblperson.ToList();
            }
            return ReturnList;
        }
        #endregion

        #region Get User by Email
        /// <summary>
        /// Takes a string with email of the User
        /// returns a list of Person with the email
        /// </summary>
        /// <param name="uemail"></param>
        /// <returns></returns>
        public static tblperson Get_UserByEmail(string uemail)
        {
            tblperson dbPerson = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    dbPerson = db.tblperson.Where(u => u.email == uemail).FirstOrDefault();
                    if (dbPerson == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }
            return dbPerson;
        }
        #endregion

        #region Get User by UserEmail
        /// <summary>
        /// takes a string email  and returns a tblperson
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static tblperson GetUserByUserEmail(string email)
        {
            tblperson dbUser = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    dbUser = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("User Does not Exist");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Writer.LogError(e);
            }

            return dbUser;
        }
        #endregion

        #region Get Rolenames by UserNameEmail
        /// <summary>
        /// takes a string Email and returns the role of the user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string GetRoleNamesByUserEmail(string email)
        {
            string role = "";

            using (var db = new ClonestoneFSEntities())
            {

                var dbUser = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                if (dbUser == null)
                {
                    Log.Writer.LogInfo("User already exists");
                    return "";
                }
                role = dbUser.userrole;
            }
            return role;

        }
        #endregion

        #region Get Number Distinct Cards owned by Email
        /// <summary>
        /// Takes the email of the User and returns the
        /// Number of the owned cards
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static int Get_NumDistinctCardsOwnedByEmail(string email)
        {
            int numCards = -1;
            using (var db = new ClonestoneFSEntities())
            {
                tblperson dbPerson = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                if (dbPerson == null)
                {
                    throw new Exception("UserDoesNotExist");
                }
                numCards = dbPerson.tblcollection.Count;
            }
            return numCards;
        }
        #endregion

        #region Get Number Total Cards Owned by Email
        /// <summary>
        /// Takes the Email adress and givesw back the number of Cards
        /// the User ownes
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static int Get_NumTotalCardsOwnedByEmail(string email)
        {
            int numCards = -1;
            using (var db = new ClonestoneFSEntities())
            {
                tblperson dbPerson = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                if (dbPerson == null)
                {
                    throw new Exception("User Does Not Exist");
                }
                numCards = 0;
                foreach (var card in dbPerson.tblcollection)
                {
                    numCards += card.numcards ?? 0;
                }
            }
            return numCards;
        }
        #endregion

        #region Get Number Of Decks owned by Email
        /// <summary>
        /// Takes a email and returns the Number of Decks
        /// owned by the user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static int Get_NumDecksOwnedByEmail(string email)
        {
            int numDecks = -1;
            using (var db = new ClonestoneFSEntities())
            {
                tblperson dbPerson = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                if (dbPerson == null)
                {
                    throw new Exception("User Does Not Exist");
                }
                numDecks = dbPerson.tbldeck.Count;
            }
            return numDecks;
        }
        #endregion

        #region Get Persons Balance by Email
        /// <summary>
        /// Takes the Email and returns the currencybalance
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static int Get_BalanceByEmail(string email)
        {
            return Get_UserByEmail(email).currencybalance ?? 0;
        }
        #endregion

        #region Get All Cards Of Collection by Email
        /// <summary>
        /// Takes the email of the user and gives back
        /// the List of the cards in the collection
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static List<tblcard> Get_AllCardsByEmail(string email)
        {
            var cardList = new List<tblcard>();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var dbUser = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("User Does Not Exist");
                    }
                    var dbCardCollection = dbUser.tblcollection.ToList();
                    if (dbCardCollection == null)
                    {
                        throw new Exception("Card Collection Not Found");
                    }
                    foreach (var cc in dbCardCollection)
                    {
                        for (int i = 0; i < cc.numcards; i++)
                            cardList.Add(cc.tbldeckcard.tblcard);
                    }
                    return cardList;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return null;
            }
        }
        #endregion

        #region Get all Decks by Email
        /// <summary>
        /// Takes a Email and gives back the Users Deck
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static List<tbldeck> Get_AllDecksByEmail(string email)
        {
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var dbPerson = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                    if (dbPerson == null)
                    {
                        throw new Exception("User Does NotExist");
                    }
                    var dbDecks = dbPerson.tbldeck.ToList();
                    if (dbDecks == null)
                    {
                        throw new Exception("No Decks Found");
                    }

                    return dbDecks;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return null;
            }
        }
        #endregion

        #region Update Balance by Email
        /// <summary>
        /// Takes the email and the new CurrencyBalance
        /// updates the database and gives back the bool if it worked out
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newCurrencyBalance"></param>
        /// <returns></returns>
        public static bool Update_BalanceByEmail(string email, int newCurrencyBalance)
        {
            var dbUser = Get_UserByEmail(email);

            dbUser.currencybalance = newCurrencyBalance;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    db.Entry(dbUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return false;
            }
        }
        #endregion

        #region Add Cards To Collection by Email
        /// <summary>
        /// Takes the email and the List of Cards
        /// and adds it to the Collection
        /// gives back a bool if it worked
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool Add_CardsToCollectionByEmail(string email, List<tblcard> cards)
        {
            var dbPerson = new tblperson();
            //try
            //{
            using (var db = new ClonestoneFSEntities())
            {
                dbPerson = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                if (dbPerson == null)
                {
                    throw new Exception("User Does Not Exist");
                }
                foreach (var card in cards)
                {
                    var userCC = (from coll in db.tblcollection
                                  where coll.fkcard == card.idcard && coll.fkperson == dbPerson.idperson
                                  select coll)
                                 .FirstOrDefault();
                    if (userCC == null) //User does not own card, add to collection
                    {
                        var cc = new tblcollection();
                        cc.tblcard = db.tblcard.Find(card.idcard);
                        cc.tblperson = dbPerson;
                        cc.numcards = 1;
                        dbPerson.tblcollection.Add(cc);
                        db.SaveChanges();
                    }
                    else //User owns card, add to num
                    {
                        userCC.numcards += 1;
                        db.Entry(userCC).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                //db.SaveChanges();
                return true;
            }
            //}
            //catch (Exception e)
            //{
            //    Writer.LogError(e);
            //    return false;
            //}
            //}
            #endregion

        }
    }
}


    


