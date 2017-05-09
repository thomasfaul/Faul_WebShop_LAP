using CardGame.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Web.Controllers.HtmlHelpers
{
    public class SortHelper
    {
        #region FilterCards
        public static List<Card> FilterCards(List<Card>Cards,int sortValue)
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
            #endregion
        }              
    }
}
