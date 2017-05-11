using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using CardGame.Web.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Web.Controllers.HtmlHelpers
{
    public class SortHelper
    {
        #region FilterCards
        public static List<Card> FilterCards(List<Card> Cards,string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                Cards = Cards.Where(s => s.Name.Contains(search)).ToList();
            }
            int sortValue = 0;
            var sorted = Cards;
            switch (sortValue)
            {
                case 1:
                    sorted = Cards.OrderBy(c => c.Name).ToList();
                    break;
                case 2:
                    sorted = Cards.OrderByDescending(c => c.Name).ToList();
                    break;
                case 3:
                    sorted = Cards.Where(c => c.IsActive == true).ToList();
                    break;
                case 4:
                    sorted = Cards.Where(c => c.IsActive == false).ToList();
                    break;
                case 5:
                    sorted = Cards.OrderBy(c => c.Type).ToList();
                    break;
                case 6:
                    sorted = Cards.OrderByDescending(c => c.Type).ToList();
                    break;
                case 7:
                    sorted = Cards.Where(c => c.Type == "Hero").OrderBy(c => c.Name).ToList();
                    break;
                case 8:
                    sorted = Cards.Where(c => c.Type == "Minion").OrderBy(c => c.Name).ToList();
                    break;
                case 9:
                    sorted = Cards.Where(c => c.Type == "Spell").OrderBy(c => c.Name).ToList();
                    break;
                case 10:
                    sorted = Cards.Where(c => c.Type == "Weapon").OrderBy(c => c.Name).ToList();
                    break;
                case 11:
                    sorted = Cards.OrderBy(c => c.Mana).ToList();
                    break;
                case 12:
                    sorted = Cards.OrderByDescending(c => c.Mana).ToList();
                    break;
                case 13:
                    sorted = Cards.OrderBy(c => c.Attack).ToList();
                    break;
                case 14:
                    sorted = Cards.OrderByDescending(c => c.Attack).ToList();
                    break;
                case 15:
                    sorted = Cards.OrderBy(c => c.Life).ToList();
                    break;
                case 16:
                    sorted = Cards.OrderByDescending(c => c.Life).ToList();
                    break;
                default:
                    sorted = Cards.ToList();
                    break;
            }
            return sorted;

        }
        #endregion

        #region FilterCardsII
        public static List<Card> FilterCards(List<Card> Cards, int sortValue)
        {

            var sorted = Cards;
            switch (sortValue)
            {
                case 1:
                    sorted = Cards.OrderBy(c => c.Name).ToList();
                    break;
                case 2:
                    sorted = Cards.OrderByDescending(c => c.Name).ToList();
                    break;
                case 3:
                    sorted = Cards.Where(c => c.IsActive == true).ToList();
                    break;
                case 4:
                    sorted = Cards.Where(c => c.IsActive == false).ToList();
                    break;
                case 5:
                    sorted = Cards.OrderBy(c => c.Type).ToList();
                    break;
                case 6:
                    sorted = Cards.OrderByDescending(c => c.Type).ToList();
                    break;
                case 7:
                    sorted = Cards.Where(c => c.Type == "Hero").OrderBy(c => c.Name).ToList();
                    break;
                case 8:
                    sorted = Cards.Where(c => c.Type == "Minion").OrderBy(c => c.Name).ToList();
                    break;
                case 9:
                    sorted = Cards.Where(c => c.Type == "Spell").OrderBy(c => c.Name).ToList();
                    break;
                case 10:
                    sorted = Cards.Where(c => c.Type == "Weapon").OrderBy(c => c.Name).ToList();
                    break;
                case 11:
                    sorted = Cards.OrderBy(c => c.Mana).ToList();
                    break;
                case 12:
                    sorted = Cards.OrderByDescending(c => c.Mana).ToList();
                    break;
                case 13:
                    sorted = Cards.OrderBy(c => c.Attack).ToList();
                    break;
                case 14:
                    sorted = Cards.OrderByDescending(c => c.Attack).ToList();
                    break;
                case 15:
                    sorted = Cards.OrderBy(c => c.Life).ToList();
                    break;
                case 16:
                    sorted = Cards.OrderByDescending(c => c.Life).ToList();
                    break;
                default:
                    sorted = Cards.ToList();
                    break;
            }
            return sorted;

        }
        #endregion

        #region FilterUsers
        public static List<AdminUserInfo> FilterUsers(List<AdminUserInfo> Users, string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                Users = Users.Where(u => u.Firstname.Contains(search)||u.Lastname.Contains(search)||u.Gamertag.Contains(search)||u.Email.Contains(search)).ToList();

            }
            int sortValue = 0;
            var sorted = Users;
            switch (sortValue)
            {
                case 1:
                    sorted = Users.OrderBy(c => c.Firstname).ToList();
                    break;
                case 2:
                    sorted = Users.OrderByDescending(c => c.Firstname).ToList();
                    break;  
                case 3:      
                    sorted = Users.OrderBy(c => c.Lastname).ToList();
                    break;   
                case 4:      
                    sorted = Users.OrderByDescending(c => c.Lastname).ToList();
                    break;   
                case 5:      
                    sorted = Users.OrderBy(c => c.Gamertag).ToList();
                    break;   
                case 6:      
                    sorted = Users.OrderByDescending(c => c.Gamertag).ToList();
                    break;   
                case 7:      
                    sorted = Users.OrderBy(c => c.Email).ToList();
                    break;   
                case 8:      
                    sorted = Users.OrderByDescending(c => c.Email).ToList();
                    break;   
                case 9:      
                    sorted = Users.Where(c => c.IsActive == true).ToList();
                    break;   
                case 10:     
                    sorted = Users.Where(c => c.IsActive == false).ToList();
                    break;   
                case 11:     
                    sorted = Users.OrderBy(c => c.EntryDate).ToList();
                    break;
                case 12:
                    sorted = Users.OrderByDescending(c => c.EntryDate).ToList();
                    break;
                case 13:
                    sorted = Users.OrderBy(c => c.BanDate).ToList();
                    break;
                case 14:
                    sorted = Users.OrderByDescending(c => c.BanDate).ToList();
                    break;
                case 15:
                    sorted = Users.OrderBy(c => c.userrole).ToList();
                    break;
                case 16:
                    sorted = Users.OrderByDescending(c => c.userrole).ToList();
                    break;
                case 17:
                    sorted = Users.Where(c => c.userrole=="player").ToList();
                    break;
                case 18:
                    sorted = Users.Where(c => c.userrole == "admin").ToList();
                    break;
                case 19:
                    sorted = Users.OrderBy(c => c.CurrencyBalance).ToList();
                    break;
                case 20:
                    sorted = Users.OrderByDescending(c => c.CurrencyBalance).ToList();
                    break;
                default:
                    sorted = Users.ToList();
                    break;
            }
            return sorted;

        }
        #endregion

        #region FilterUsersII
        public static List<AdminUserInfo> FilterUsers(List<AdminUserInfo> Users, int sortValue)
        {
            var sorted = Users;
            switch (sortValue)
            {
                case 1:
                    sorted = Users.OrderBy(c => c.Firstname).ToList();
                    break;
                case 2:
                    sorted = Users.OrderByDescending(c => c.Firstname).ToList();
                    break;
                case 3:
                    sorted = Users.OrderBy(c => c.Lastname).ToList();
                    break;
                case 4:
                    sorted = Users.OrderByDescending(c => c.Lastname).ToList();
                    break;
                case 5:
                    sorted = Users.OrderBy(c => c.Gamertag).ToList();
                    break;
                case 6:
                    sorted = Users.OrderByDescending(c => c.Gamertag).ToList();
                    break;
                case 7:
                    sorted = Users.OrderBy(c => c.Email).ToList();
                    break;
                case 8:
                    sorted = Users.OrderByDescending(c => c.Email).ToList();
                    break;
                case 9:
                    sorted = Users.Where(c => c.IsActive == true).ToList();
                    break;
                case 10:
                    sorted = Users.Where(c => c.IsActive == false).ToList();
                    break;
                case 11:
                    sorted = Users.OrderBy(c => c.EntryDate).ToList();
                    break;
                case 12:
                    sorted = Users.OrderByDescending(c => c.EntryDate).ToList();
                    break;
                case 13:
                    sorted = Users.OrderBy(c => c.BanDate).ToList();
                    break;
                case 14:
                    sorted = Users.OrderByDescending(c => c.BanDate).ToList();
                    break;
                case 15:
                    sorted = Users.OrderBy(c => c.userrole).ToList();
                    break;
                case 16:
                    sorted = Users.OrderByDescending(c => c.userrole).ToList();
                    break;
                case 17:
                    sorted = Users.Where(c => c.userrole == "player").ToList();
                    break;
                case 18:
                    sorted = Users.Where(c => c.userrole == "admin").ToList();
                    break;
                case 19:
                    sorted = Users.OrderBy(c => c.CurrencyBalance).ToList();
                    break;
                case 20:
                    sorted = Users.OrderByDescending(c => c.CurrencyBalance).ToList();
                    break;
                default:
                    sorted = Users.ToList();
                    break;
            }
            return sorted;

        }
        #endregion

        #region FilterCardPacks
        public static List<CardPack> FilterCardPacks(List<CardPack> Packs, string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                Packs = Packs.Where(s => s.PackName.Contains(search)).ToList();
            }
            int sortValue = 0;
            var sorted = Packs;
            switch (sortValue)
            {
                case 1:
                    sorted = Packs.OrderBy(c => c.PackName).ToList();
                    break;
                case 2:
                    sorted = Packs.OrderByDescending(c => c.PackName).ToList();
                    break;
                case 3:
                    sorted = Packs.Where(c => c.IsActive == true).ToList();
                    break;
                case 4:
                    sorted = Packs.Where(c => c.IsActive == false).ToList();
                    break;
                case 5:
                    sorted = Packs.OrderBy(c => c.NumCards).ToList();
                    break;
                case 6:
                    sorted = Packs.OrderByDescending(c => c.NumCards).ToList();
                    break;

                case 7:
                    sorted = Packs.OrderBy(c => c.PackPrice).ToList();
                    break;
                case 8:
                    sorted = Packs.OrderByDescending(c => c.PackPrice).ToList();
                    break;
                case 9:
                    sorted = Packs.OrderBy(c => c.Worth).ToList();
                    break;
                case 10:
                    sorted = Packs.OrderByDescending(c => c.Worth).ToList();
                    break;
                case 11:
                    sorted = Packs.Where(c => c.IsMoney == true).ToList();
                    break;
                case 12:
                    sorted = Packs.Where(c => c.IsMoney == false).ToList();
                    break;
                default:
                    sorted = Packs.ToList();
                    break;
            }
            return sorted;
        }
        #endregion

        #region FilterCardPacks II
        public static List<CardPack> FilterCardPacks(List<CardPack> Packs, int sortValue)
        {
            var sorted = Packs;
            switch (sortValue)
            {
                case 1:
                    sorted = Packs.OrderBy(c => c.PackName).ToList();
                    break;
                case 2:
                    sorted = Packs.OrderByDescending(c => c.PackName).ToList();
                    break;
                case 3:
                    sorted = Packs.Where(c => c.IsActive == true).ToList();
                    break;
                case 4:
                    sorted = Packs.Where(c => c.IsActive == false).ToList();
                    break;
                case 5:
                    sorted = Packs.OrderBy(c => c.NumCards).ToList();
                    break;
                case 6:
                    sorted = Packs.OrderByDescending(c => c.NumCards).ToList();
                    break;

                case 7:
                    sorted = Packs.OrderBy(c => c.PackPrice).ToList();
                    break;
                case 8:
                    sorted = Packs.OrderByDescending(c => c.PackPrice).ToList();
                    break;
                case 9:
                    sorted = Packs.OrderBy(c => c.Worth).ToList();
                    break;
                case 10:
                    sorted = Packs.OrderByDescending(c => c.Worth).ToList();
                    break;
                case 11:
                    sorted = Packs.Where(c => c.IsMoney == true).ToList();
                    break;
                case 12:
                    sorted = Packs.Where(c => c.IsMoney == false).ToList();
                    break;
                default:
                    sorted = Packs.ToList();
                    break;
            }
            return sorted;
        }
        #endregion


        #region FilterOrders
        public static List<OrderInfo> FilterOrders(List<OrderInfo> Orders, int sortValue)
        {

            var sorted = Orders;
            switch (sortValue)
            {
                case 1:
                    sorted = Orders.OrderBy(c => c.ID).ToList();
                    break;
                case 2:
                    sorted = Orders.OrderByDescending(c => c.ID).ToList();
                    break;
                case 3:
                    sorted = Orders.Where(c => c.isActive == true).ToList();
                    break;
                case 4:
                    sorted = Orders.Where(c => c.isActive == false).ToList();
                    break;
                case 5:
                    sorted = Orders.OrderBy(c => c.NumberOfPackages).ToList();
                    break;
                case 6:
                    sorted = Orders.OrderByDescending(c => c.NumberOfPackages).ToList();
                    break;

                case 7:
                    sorted = Orders.OrderBy(c => c.TotalCost).ToList();
                    break;
                case 8:
                    sorted = Orders.OrderByDescending(c => c.TotalCost).ToList();
                    break;
                case 9:
                    sorted = Orders.OrderBy(c => c.CreditCard).ToList();
                    break;
                case 10:
                    sorted = Orders.OrderByDescending(c => c.CreditCard).ToList();
                    break;
                case 11:
                    sorted = Orders.OrderBy(c => c.User.Firstname).ToList();
                    break;
                case 12:
                    sorted = Orders.OrderByDescending(c => c.User.Firstname).ToList();
                    break;
                case 13:
                    sorted = Orders.OrderBy(c => c.User.Lastname).ToList();
                    break;
                case 14:
                    sorted = Orders.OrderByDescending(c => c.User.Lastname).ToList();
                    break;
                case 15:
                    sorted = Orders.OrderBy(c => c.User.Email).ToList();
                    break;
                case 16:
                    sorted = Orders.OrderByDescending(c => c.User.Email).ToList();
                    break;
                default:
                    sorted = Orders.ToList();
                    break;
            }
            return sorted;
        }
         #endregion

        #region FilterOrdersII
        public static List<OrderInfo> FilterOrders(List<OrderInfo> Orders, string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                Orders = Orders.Where(o => o.User.Lastname.Contains(search) || o.User.Firstname.Contains(search)).ToList();
            }
            int sortValue = 0;
            var sorted = Orders;
            switch (sortValue)
            {
                case 1:
                    sorted = Orders.OrderBy(c => c.ID).ToList();
                    break;
                case 2:
                    sorted = Orders.OrderByDescending(c => c.ID).ToList();
                    break;
                case 3:
                    sorted = Orders.Where(c => c.isActive == true).ToList();
                    break;
                case 4:
                    sorted = Orders.Where(c => c.isActive == false).ToList();
                    break;
                case 5:
                    sorted = Orders.OrderBy(c => c.NumberOfPackages).ToList();
                    break;
                case 6:
                    sorted = Orders.OrderByDescending(c => c.NumberOfPackages).ToList();
                    break;

                case 7:
                    sorted = Orders.OrderBy(c => c.TotalCost).ToList();
                    break;
                case 8:
                    sorted = Orders.OrderByDescending(c => c.TotalCost).ToList();
                    break;
                case 9:
                    sorted = Orders.OrderBy(c => c.CreditCard).ToList();
                    break;
                case 10:
                    sorted = Orders.OrderByDescending(c => c.CreditCard).ToList();
                    break;
                case 11:
                    sorted = Orders.OrderBy(c => c.User.Firstname).ToList();
                    break;
                case 12:
                    sorted = Orders.OrderByDescending(c => c.User.Firstname).ToList();
                    break;
                case 13:
                    sorted = Orders.OrderBy(c => c.User.Lastname).ToList();
                    break;
                case 14:
                    sorted = Orders.OrderByDescending(c => c.User.Lastname).ToList();
                    break;
                case 15:
                    sorted = Orders.OrderBy(c => c.User.Email).ToList();
                    break;
                case 16:
                    sorted = Orders.OrderByDescending(c => c.User.Email).ToList();
                    break;
                default:
                    sorted = Orders.ToList();
                    break;
            }
            return sorted;
           
        }
        #endregion
    }
}
