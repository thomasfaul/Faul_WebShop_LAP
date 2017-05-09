using CardGame.Web.Models;
using CardGame.Web.Models.DB;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Web.Controllers.HtmlHelpers
{
    public class SortHelper
    {
        #region FilterCards
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

        #region FilterCardPacks
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
            #endregion
        }



    }
}
