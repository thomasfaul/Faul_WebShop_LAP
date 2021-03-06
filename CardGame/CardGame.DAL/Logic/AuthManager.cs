﻿using System;
using System.Linq;
using CardGame.DAL.Model;
using System.Diagnostics;
using log4net;
using System.Data.Entity;

namespace CardGame.DAL.Logic
{
    public class AuthManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Register
        /// <summary>
        /// 
        /// Gets the registered person,
        /// looks if the user already exists,
        /// hashes the password with the helper method,
        /// sends it to the db
        /// </summary>
        /// <param name="regUser"></param>
        /// <returns>bool</returns>
        public static bool Register(User regUser)
        {
            log.Info("AuthManager-Register");
            try
            {
                using (var db = new itin21_ClonestoneFSEntities())
                {
                    if (db.AllUsers.Any(n => n.Email == regUser.Email))
                    {
                        log.Error("AuthManager-Register, Emailadresse gibt es bereits");
                        throw new Exception("User-Emailadresse gibt es bereits");
                        

                    }
                    if (db.AllUsers.Any(n => n.Email == regUser.GamerTag))
                    {
                        log.Error("AuthManager-Register, Gamertag gibt es bereits");
                        throw new Exception("User-Gamertag gibt es bereits");

                    }
                    string salt = Helper.GenerateSalt();

                    //Passwort Hashen
                    string hashedAndSaltedPassword = Helper.GenerateHash(regUser.Password + salt);

                    regUser.Password = hashedAndSaltedPassword;
                    regUser.Salt = salt;

                    db.AllUsers.Add(regUser);
                    db.SaveChanges();
                }
            } 
            
            catch (Exception e)
            {
                //Debugger.Break();
                log.Error("AuthManager-Register",e);
                return false; 
                       
            }

            return true;
        }
        #endregion

        #region Auth(enticate) User
        /// <summary>
        /// takes string email and password
        /// hashes both,
        /// compares them
        /// returns bool if it worked
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        public static bool AuthUser(string email, string password)
        {
            log.Info("AuthManager-AuthUser");
            try
            {
                string dbUserPassword = null;
                string dbUserSalt = null;

                using (var db = new itin21_ClonestoneFSEntities())
                {
                    User dbUser = db.AllUsers.Where(u => u.Email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("User gibt es noch nicht, bitte Registrieren");
                    }

                    dbUserPassword = dbUser.Password;
                    dbUserSalt = dbUser.Salt;

                    log.Info("Entered Pass = " + password);

                    password = Helper.GenerateHash(password + dbUserSalt);

                    log.Info("HashPass = " + password);

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
                
                log.Error("AuthManager-AuthUser", e);
                return false;
            }
        }

        #endregion


        #region ResetPassword
        /// <summary>
        /// 
        /// Hashes the password and salt and changes the Stte of the database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="salt"></param>
        /// <param name="hashedpassword"></param>
        /// <returns>bool</returns>
        public static bool ResetThePassword(string hashedpassword,string salt,string email)
        {
            log.Info("AuthManager-Resetpassword");
            try
            {
                var dbUser = UserManager.Get_UserByEmail(email);

                using (var db = new itin21_ClonestoneFSEntities())
                {
                    dbUser.Password = hashedpassword;
                    dbUser.Salt = salt;
                    db.Entry(dbUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }

            catch (Exception e)
            {
                //Debugger.Break();
                log.Error("AuthManager-Register", e);
                return false;

            }
        }
        #endregion
    }
}
