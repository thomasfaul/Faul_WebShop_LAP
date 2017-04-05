using System;
using System.Linq;
using CardGame.DAL.Model;
using CardGame.Log;

namespace CardGame.DAL.Logic
{
    public class AuthManager
    {
        public static bool Register(tblperson regUser)
        {
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    if (db.tblperson.Any(n => n.email == regUser.email))
                    {
                        throw new Exception("User-Emailadresse gibt es bereits");
                    }
                    //Salt erzeugen
                    string salt = Helper.GenerateSalt();

                    //Passwort Hashen
                    string hashedAndSaltedPassword = Helper.GenerateHash(regUser.password + salt);

                    regUser.password = hashedAndSaltedPassword;
                    regUser.salt = salt;

                    db.tblperson.Add(regUser);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e); 
                return false; 
                //TODO Errorpage             
            }

            return true;
        }

        public static bool AuthUser(string email, string password)
        {
            try
            {
                string dbUserPassword = null;
                string dbUserSalt = null;

                using (var db = new ClonestoneFSEntities())
                {
                    tblperson dbUser = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("User gibt es noch nicht, bitte Registrieren");
                    }

                    dbUserPassword = dbUser.password;
                    dbUserSalt = dbUser.salt;

                   Writer.LogInfo("Entered Pass = " + password);

                    password = Helper.GenerateHash(password + dbUserSalt);

                    Writer.LogInfo("HashPass = " + password);

                    if (dbUserPassword == password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return false; 
            }
        }
    }
}
