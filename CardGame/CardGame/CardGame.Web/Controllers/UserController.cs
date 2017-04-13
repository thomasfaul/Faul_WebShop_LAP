﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.Models.UI;

namespace CardGame.Web.Controllers
{
    public class UserController : Controller
    {

        #region ACTIONRESULT ADIM- USER 
        /// <summary>
        /// Returns the Usersdata for the Adminpage
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            List<Register> UserList = new List<Register>();
            var dbUserlist = UserManager.GetAllUser();

            foreach (var c in dbUserlist)
            {
                Register user = new Register();
                user.ID = c.idperson;
                user.Firstname = c.firstname;
                user.Lastname = c.lastname;
                user.Email = c.email;
                user.Role = c.userrole;
                user.Passwort = c.password;
                user.Salt = c.salt;
                if (c.currencybalance != null)
                {
                    user.Currencybalance = (int)c.currencybalance;
                }
                else
                {
                    user.Currencybalance = 0;
                }
                UserList.Add(user);

            }
            return View(UserList);
        } 
        #endregion

    }
}