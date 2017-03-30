using System.Collections.Generic;
using CardGame.Domain.Entities;

namespace CardGame.Domain.Abstract
{
    public interface IProductsRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
