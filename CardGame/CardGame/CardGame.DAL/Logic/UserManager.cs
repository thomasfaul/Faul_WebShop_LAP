using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;

namespace CardGame.DAL.Logic
{
    public class UserManager
    {
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
    }
}

