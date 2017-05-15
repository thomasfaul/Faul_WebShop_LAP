using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using System.Data.Entity;
using System.Diagnostics;
using log4net;

namespace CardGame.DAL.Logic
{
    public class UserManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly Dictionary<int, string> CardClassNames;
        public static readonly Dictionary<int, string> CardTypeNames;


        #region Constructor USERMANAGER
        static UserManager()
        {
            log.Info("Usermanager - Konstruktor");
            CardClassNames = new Dictionary<int, string>();
            CardTypeNames = new Dictionary<int, string>();
            List<CardType> cardTypeList = null;
            List<CardClass> cardClassList = null;

            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    cardTypeList = db.AllCardTypes.ToList();
                    cardClassList = db.AllCardClasses.ToList();
                }
                foreach (var type in cardTypeList)
                {
                    CardTypeNames.Add(type.ID, type.Name);
                }
                foreach (var clas in cardClassList)
                {
                    CardTypeNames.Add(clas.ID, clas.Name);
                };
                CardTypeNames.Add(0, "n/a");
                CardClassNames.Add(0, "n/a");
            }
            catch (Exception ex)
            {
                //Debugger.Break();
                log.Error("Usermanager-Konstruktor", ex);

            }

        }
        #endregion

        #region GetAllUser
        /// <summary>
        /// Connects to the Db and gets all Users
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUser()
        {
            log.Info("Usermanager-Get_GetAllUser");
            List<User> ReturnList = null;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                // TODO - Include
                // .Include(t => t.tabelle) um einen Join zu machen !
                ReturnList = db.AllUsers.ToList();
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
        public static User Get_UserByEmail(string uemail)
        {
            log.Info("Usermanager-Get_UserbyEmail");
            User dbPerson = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    dbPerson = db.AllUsers.Where(u => u.Email == uemail).FirstOrDefault();
                    if (dbPerson == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Get_UserbyEmail", e);
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
        public static User GetUserByUserEmail(string email)
        {
            log.Info("Usermanager-Get_UserbyUserEmail");
            User dbUser = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    dbUser = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        log.Error(" Get User by UserEmail-User existiert nicht");
                        throw new Exception("User Does not Exist");
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("GetUserByUserEmail-User hat sich die Agb´s angesehen", e);
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
            log.Info("Usermanager-GetRoleNamesByUserEmail");
            string role = "";

            using (var db = new itin21_ClonestoneFSEntities())
            {

                var dbUser = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                if (dbUser == null)
                {
                    log.Error("UserManager-User already exists");
                    return "";
                }
                role = dbUser.UserRole;
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
            log.Info("Usermanager - Get_NumDistinctCardsOwnedByEmail");
            int numCards = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                User dbPerson = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                if (dbPerson == null)
                {
                    log.Error("Usermanager-User does not exists");
                    throw new Exception("UserDoesNotExist");
                }
                numCards = dbPerson.AllUserCardCollections.Count;
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
            log.Info("Usermanager-Get_NumTotalCardsOwnedByEmail");
            int numCards = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                User dbPerson = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                if (dbPerson == null)
                {
                    log.Error("Usermanager-Get_NumTotalCardsOwnedByEmail, User does not exist");
                    throw new Exception("User Does Not Exist");
                }
                numCards = 0;
                foreach (var card in dbPerson.AllUserCardCollections)
                {
                    numCards += card.NumberOfCards ?? 0;
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
            log.Info("Usermanager-Get_NumDecksOwnedByEmail");
            int numDecks = -1;
            using (var db = new itin21_ClonestoneFSEntities())
            {
                User dbPerson = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                if (dbPerson == null)
                {
                    log.Error("Usermanager-Get_NumDecksOwnedByEmail, User does not exist");
                    throw new Exception("User Does Not Exist");
                }
                numDecks = dbPerson.AllDecks.Count;
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
            log.Info("Usermanager-Get_BalanceByEmail");
            return Get_UserByEmail(email).AmountMoney ?? 0;
        }
        #endregion

        #region Get All Cards Of Collection by Email
        /// <summary>
        /// Takes the email of the user and gives back
        /// the List of the cards in the collection
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static List<Card> Get_AllCardsByEmail(string email)
        {
            log.Info("Usermanager-Get_AllCardsByEmail");

            var cardList = new List<Card>();

            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var dbUser = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        log.Error("Usermanager-Get_AllCardsByEmail, User does not exist");
                        throw new Exception("User Does Not Exist");
                    }
                    var dbCardCollection = dbUser.AllUserCardCollections.ToList();
                    if (dbCardCollection == null)
                    {
                        log.Error("Usermanager-Get_AllCardsByEmail, Card Collection not found");
                        throw new Exception("Card Collection Not Found");
                    }
                    foreach (var cc in dbCardCollection)
                    {
                        for (int i = 0; i < cc.NumberOfCards; i++)
                            cardList.Add(cc.Card);
                    }
                    return cardList;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-Get_AllCardsByEmail", e);
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
        public static List<Deck> Get_AllDecksByEmail(string email)
        {
            log.Info("Get_AllDecksByEmail");
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    var dbPerson = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                    if (dbPerson == null)
                    {
                        log.Error("Usermanager-Get_AllDecksByEmail, User does not exist");
                        throw new Exception("User Does Not Exist");
                    }
                    var dbDecks = dbPerson.AllDecks.ToList();
                    if (dbDecks == null)
                    {
                        log.Error("Usermanager-Get_AllDecksByEmail, No decks found");
                        throw new Exception("No Decks Found");
                    }

                    return dbDecks;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Usermanager-Get_AllDecksByEmail", e);
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
            log.Info("UserManager-Update_BalanceByEmail");
            var dbUser = Get_UserByEmail(email);

            dbUser.AmountMoney = newCurrencyBalance;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    db.Entry(dbUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("UserManager-Update_BalanceByEmail", e);
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
        public static bool Add_CardsToCollectionByEmail(string email, List<Card> cards)
        {
            log.Info("UserManager-Add_CardsToCollectionByEmail");
            var dbPerson = new User();
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    dbPerson = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                    if (dbPerson == null)
                    {
                        log.Error("UserManager-Add_CardsToCollectionByEmail, User does not exist");
                        throw new Exception("User Does Not Exist");
                    }
                    foreach (var card in cards)
                    {
                        var userCC = (from coll in db.AllUserCardCollections
                                      where coll.ID_Card == card.ID && coll.ID_User == dbPerson.ID
                                      select coll)
                                     .FirstOrDefault();
                        if (userCC == null) //User does not own card, add to collection
                        {
                            var cc = new UserCardCollection();
                            cc.Card = db.AllCards.Find(card.ID);
                            cc.User = dbPerson;
                            cc.NumberOfCards = 1;
                            dbPerson.AllUserCardCollections.Add(cc);
                            db.SaveChanges();
                        }
                        else //User owns card, add to num
                        {
                            userCC.NumberOfCards += 1;
                            db.Entry(userCC).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("UserManager-Add_CardsToCollectionByEmail,", e);
                return false;
            }


        }
        #endregion

        #region SET USER INACTIVE
        /// <summary>
        /// Sets a user inactive
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool SetUserInActive(int id)
        {
            using (var db = new itin21_ClonestoneFSEntities())
            {
                User dbuser = db.AllUsers.SingleOrDefault(p => p.ID == id);
                dbuser.IsActive = false;

                db.Entry(dbuser).State = EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }
        #endregion

        #region Get User by ID
        /// <summary>
        /// Takes a string with email of the User
        /// returns a list of Person with the email
        /// </summary>
        /// <param name="uemail"></param>
        /// <returns></returns>
        public static User Get_UserById(int id)
        {
            log.Info("Usermanager-Get_UserbyID");
            User dbPerson = null;
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    dbPerson = db.AllUsers.Where(u => u.ID == id).FirstOrDefault();
                    if (dbPerson == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                log.Error("Get_UserbyID", e);
            }
            return dbPerson;
        }
        #endregion

        #region SAVE USER
        /// <summary>
        /// Saves a new User or Modifies a user 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <param name="Lastname"></param>
        /// <param name="email"></param>
        /// <param name="gamertag"></param>
        /// <param name="isactive"></param>
        /// <param name="pic"></param>
        /// <param name="currencybal"></param>
        /// <param name="mimetypename"></param>
        public static void SaveAUser(int id, string Firstname, string Lastname, string Email, string Gamertag, bool IsActive, byte[] Pic, string ImageMimeType, int CurrencyBalance, string userrole)
        {
            if (id == 0)
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    User dbuser = new User();
                    dbuser.ID = id;
                    dbuser.FirstName = Firstname;
                    dbuser.LastName = Lastname;
                    dbuser.Email = Email;
                    dbuser.GamerTag = Gamertag;
                    dbuser.IsActive = IsActive;
                    dbuser.Avatar = Pic;
                    dbuser.AvatarMimeType = ImageMimeType;
                    dbuser.AmountMoney = CurrencyBalance;
                    dbuser.UserRole = userrole;
                    dbuser.EntryDate = DateTime.Now;

                    db.AllUsers.Add(dbuser);

                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    if (db.AllPacks != null)
                    {
                        User dbuser = db.AllUsers.SingleOrDefault(u => u.ID == id); ;
                        dbuser.ID = id;
                        dbuser.FirstName = Firstname;
                        dbuser.LastName = Lastname;
                        //dbuser.Email = Email;
                        dbuser.GamerTag = Gamertag;
                        dbuser.IsActive = IsActive;
                        dbuser.Avatar = Pic;
                        dbuser.AvatarMimeType = ImageMimeType;
                        dbuser.AmountMoney = CurrencyBalance;
                        dbuser.UserRole = userrole;


                        db.Entry(dbuser).State = EntityState.Modified;

                        db.SaveChanges();

                    }


                }
            }
        }
        #endregion
        #region SAVE USERII
        /// <summary>
        /// Saves a new User or Modifies a user 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <param name="Lastname"></param>
        /// <param name="email"></param>
        /// <param name="gamertag"></param>
        /// <param name="isactive"></param>
        /// <param name="pic"></param>
        /// <param name="currencybal"></param>
        /// <param name="mimetypename"></param>
        public static void SaveAUser(int id, string Firstname, string Lastname, string Email, string Password,string Gamertag, byte[] Pic, string ImageMimeType)
        {
            if (id == 0)
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    User dbuser = new User();
                    dbuser.ID = id;
                    dbuser.FirstName = Firstname;
                    dbuser.LastName = Lastname;
                    dbuser.Email = Email;
                    dbuser.GamerTag = Gamertag;
                    dbuser.Avatar = Pic;
                    dbuser.AvatarMimeType = ImageMimeType;
                    dbuser.EntryDate = DateTime.Now;

                    db.AllUsers.Add(dbuser);

                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {

                    if (db.AllUsers != null)
                    {
                        if (db.AllUsers.Any(n => n.Email == Email))
                        {
                            log.Error("AuthManager-Register, Emailadresse gibt es bereits");
                            throw new Exception("User-Emailadresse gibt es bereits");
                        }
                        string salt = Helper.GenerateSalt();
                        string hashedAndSaltedPassword = Helper.GenerateHash(Password + salt);
                        User dbuser = db.AllUsers.SingleOrDefault(u => u.ID == id); 
                        dbuser.ID = id;
                        dbuser.Password= Helper.GenerateHash(Password + salt);
                        dbuser.FirstName = Firstname;
                        dbuser.LastName = Lastname;
                        dbuser.GamerTag = Gamertag;
                        dbuser.Avatar = Pic;
                        dbuser.AvatarMimeType = ImageMimeType;


                        db.Entry(dbuser).State = EntityState.Modified;

                        db.SaveChanges();

                    }
                }
            }
        }
        #endregion



    }
}