﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.DAL.Model;
using CardGame.Log;
using System.Data.Entity;

namespace CardGame.DAL.Logic
{
    public class UserManager
    {
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
        public static string GetRoleNamesByUserName(string email)
        {
            string role = "";

            using (var db = new ClonestoneFSEntities())
            {

                var dbUser = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                if (dbUser == null)
                {
                    throw new Exception("Userdoesnotexists");//TODO ERRORHANDLING
                }
                role = dbUser.userrole;
            }
            return role;

        }
    }
}

