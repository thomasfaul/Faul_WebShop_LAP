﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        throw new Exception("UserAlreadxExists");
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
                throw;
                //Writer.LogError(e);            
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
                        throw new Exception("UserDoesNotExist");
                    }

                    dbUserPassword = dbUser.password;
                    dbUserSalt = dbUser.salt;

                    Log.Writer.LogInfo("Entered Pass = " + password);

                    password = Helper.GenerateHash(password + dbUserSalt);

                    Log.Writer.LogInfo("HashPass = " + password);

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

                throw;
            }
        }
    }
}
